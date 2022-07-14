using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Models.Entities;
using PersonalSite.Core.Models.Entities.Enums;
using PersonalSite.Infrastructure.Common.Models;
using PersonalSite.Infrastructure.EF;
using PersonalSite.Services.Auth.Models;
using PersonalSite.Services.Auth.Services;

namespace PersonalSite.Services.Auth;

public class AuthFacade : IAuthFacade
{
    private readonly AuthConfig _authConfig;
    private readonly ApplicationContext _context;
    private readonly TokenGenerator _tokenGenerator;
    private readonly GoogleApi _googleApi;
    private readonly ProfileUpdater _profileUpdater;

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

        return Result<AuthInfo>.Success(new AuthInfo(creds.Profile, token));
    }

    public async Task<Result<AuthInfo>> AuthorizeByGoogleAsync(string authCode)
    {
        ArgumentNullException.ThrowIfNull(authCode);

        var tokenResult = await _googleApi.AuthAsync(authCode);
        if (!tokenResult.IsSuccess)
            return Result<AuthInfo>.FromFail(tokenResult);

        var gProfileResult = await _googleApi.GetProfile(tokenResult.Value);
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

        await _context.SaveChangesAsync();
        var token = _tokenGenerator.Generate(_authConfig.PrivateKey, profile.Id);
        return Result<AuthInfo>.Success(new AuthInfo(profile, token));
    }
}