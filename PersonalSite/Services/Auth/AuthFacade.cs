using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Models.Entities;
using PersonalSite.Infrastructure.Common.Models;
using PersonalSite.Infrastructure.EF;
using PersonalSite.Services.Auth.Models;
using PersonalSite.Services.Auth.Services;
using Serilog;
using Serilog.Events;

namespace PersonalSite.Services.Auth;

public class AuthFacade : IAuthFacade
{
    private readonly AuthConfig _authConfig;
    private readonly ApplicationContext _context;
    private readonly TokenGenerator _tokenGenerator;
    private readonly GoogleApi _googleApi;
    private readonly ProfileUpdater _profileUpdater;
    
    readonly bool _isDebug = Log.IsEnabled(LogEventLevel.Debug);

    public AuthFacade(
        AuthConfig authConfig,
        ApplicationContext context,
        TokenGenerator tokenGenerator,
        GoogleApi googleApi,
        ProfileUpdater profileUpdater)
    {
        _authConfig = authConfig;
        _context = context;
        _tokenGenerator = tokenGenerator;
        _googleApi = googleApi;
        _profileUpdater = profileUpdater;
    }

    public async Task<Result<ProfileEntity>> RegisterAsync(string email, string password, string nickname)
    {
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(password);
        ArgumentNullException.ThrowIfNull(nickname);

        var existed = await _context.Profiles
            .Include(x => x.ProfileCredentials)
            .FirstOrDefaultAsync(x =>
                x.Nickname == nickname || x.ProfileCredentials.Email == email);

        if (existed != null)
        {
            if (string.Equals(nickname, existed.Nickname))
                return Result<ProfileEntity>.Fail($@"The nickname: {nickname} already exists");
            if (string.Equals(email, existed.ProfileCredentials.Email))
                return Result<ProfileEntity>.Fail($"The email: {email} already exists");
            return Result<ProfileEntity>.Fail($"The account already exists");
        }

        var profile = _profileUpdater.CreateNewOne(nickname, email, password);
        
        _context.Profiles.Add(profile);
        
        await _context.SaveChangesAsync();
        return Result<ProfileEntity>.Success(profile);
    }

    public async Task<Result<AuthInfo>> AuthorizeAsync(string email, string password)
    {
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(password);

        var creds = await _context.ProfileCredentials
            .Include(x => x.Profile)
            .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        if (creds == null)
            return Result<AuthInfo>.Fail($"Login failed: try different credentials");

        var token = _tokenGenerator.Generate(_authConfig.PrivateKey, creds.Profile.Id);
        
        creds.Profile.RefreshToken = _tokenGenerator.GenerateRefreshToken();
        creds.Profile.RefreshTokenExpireOn = DateTime.Now.AddHours(_authConfig.RefreshTokenHoursValidity);

        await _context.SaveChangesAsync();

        return Result<AuthInfo>.Success(new AuthInfo(creds.Profile, token, creds.Profile.RefreshToken));
    }

    public async Task<Result<AuthInfo>> AuthorizeByGoogleAsync(string authCode)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(authCode);
            var sw = new Stopwatch();
            sw.Start();
            
            var tokenResult = await _googleApi.AuthAsync(authCode);
            if (_isDebug) Log.Debug("GoogleApi.AuthAsync returned {@tokenResult}", tokenResult);
            if (!tokenResult.IsSuccess)
                return Result<AuthInfo>.FromFail(tokenResult);

            var gProfileResult = await _googleApi.GetProfile(tokenResult.Value);
            if (_isDebug) Log.Debug("GoogleApi.GetProfile returned {@gProfileResult}", gProfileResult);
            if (!gProfileResult.IsSuccess)
                return Result<AuthInfo>.FromFail(tokenResult);
        
            GoogleProfile gProfile = gProfileResult.Value;
            var profile = await _context.Profiles
                .Include(x => x.GoogleProfileEntity)
                .FirstOrDefaultAsync(x =>
                    x.GoogleProfileEntity != null && 
                    x.GoogleProfileEntity.SourceId.Equals(gProfile.SourceId));
        
            if (profile == null)
            {
                profile = _profileUpdater.CreateNewOneFromGoogle(gProfile);
                _context.Profiles.Add(profile);
            }
            else
            {
                profile = _profileUpdater.UpdateProfile(profile, gProfile);
                _context.Profiles.Update(profile);
            }
        
            profile.RefreshToken = _tokenGenerator.GenerateRefreshToken();
            profile.RefreshTokenExpireOn = DateTime.Now.AddHours(_authConfig.RefreshTokenHoursValidity);

            await _context.SaveChangesAsync();
            var token = _tokenGenerator.Generate(_authConfig.PrivateKey, profile.Id);
            
            var result = Result<AuthInfo>.Success(new AuthInfo(profile, token, profile.RefreshToken));
            
            sw.Stop();
            Log.Information("Authorize by google finished in {Elapsed:000}", sw.ElapsedMilliseconds);

            return result;
        }
        catch (Exception e)
        {
            Log.Information("Authorize by google failed: {Error}", e);
            throw;
        }
    }

    public async Task<Result<AuthInfo>> RefreshToken(string refreshToken, int profileId)
    {
        var profile = await _context.Profiles.FirstAsync(x => x.Id == profileId);
        
        if (profile.RefreshToken == null || profile.RefreshTokenExpireOn == null)
            return Result<AuthInfo>.Fail("Refresh token is not valid");
        
        if (!string.Equals(profile.RefreshToken, refreshToken))
            return Result<AuthInfo>.Fail("Refresh token does not match");
        
        var token = _tokenGenerator.Generate(_authConfig.PrivateKey, profile.Id);
        var newRefreshToken = _tokenGenerator.GenerateRefreshToken();

        profile.RefreshToken = newRefreshToken;
        profile.RefreshTokenExpireOn = new DateTime();

        await _context.SaveChangesAsync();

        return Result<AuthInfo>.Success(new AuthInfo(profile, token, newRefreshToken));
    }
}