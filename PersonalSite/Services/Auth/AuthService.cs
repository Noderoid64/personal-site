using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Entities;
using PersonalSite.Infrastructure.Common.Models;
using PersonalSite.Infrastructure.EF;
using PersonalSite.Services.Auth.Models;

namespace PersonalSite.Services.Auth;

public class AuthService
{
    private readonly AuthConfig _authConfig;
    private readonly ApplicationContext _context;
    private readonly TokenGenerator _tokenGenerator;
    private readonly GoogleApi _googleApi;

    public AuthService(
        AuthConfig authConfig, 
        ApplicationContext context, 
        TokenGenerator tokenGenerator,
        GoogleApi googleApi)
    {
        _authConfig = authConfig;
        _context = context;
        _tokenGenerator = tokenGenerator;
        _googleApi = googleApi;
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

        var profile = new ProfileEntity()
        {
            Nickname = nickname,
            ProfileCredentials = new ProfileCredentialsEntity()
            {
                Email = email,
                Password = password
            }
        };
        
        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();
        return Result<ProfileEntity>.Success(profile);
    }
    
    public async Task<Result<(ProfileEntity profile, string token)>> AuthorizeAsync(string email, string password)
    {
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(password);
        
        var creds = await _context.ProfileCredentials
            .Include(x => x.Profile)
            .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        if (creds == null)
            return Result<(ProfileEntity profile, string token)>.Fail($"Login failed: try different credentials");
        
        var token = _tokenGenerator.Generate(
            _authConfig.PrivateKey, 
            creds.Profile.Id.ToString(), 
            false);
        
        return Result<(ProfileEntity profile, string token)>.Success((creds.Profile, token));
    }

    public async Task<Result<(ProfileEntity profile, string token)>> AuthorizeByGoogleAsync(string authCode)
    {
        ArgumentNullException.ThrowIfNull(authCode);
        
        var tokenResult = await _googleApi.AuthAsync(authCode);
        if (tokenResult.IsSuccess)
        {
            var gProfileResult = await _googleApi.GetProfile(tokenResult.Value);
            if (gProfileResult.IsSuccess)
            {
                GoogleProfile gProfile = gProfileResult.Value;
                var profile = await _context.Profiles
                    .Include(x => x.GoogleProfileEntity)
                    .FirstOrDefaultAsync(x => x.GoogleProfileEntity != null && x.GoogleProfileEntity.SourceId.Equals(gProfile.SourceId));
                if (profile == null)
                {
                    profile = CreateNewOne(gProfile);
                    _context.Profiles.Add(profile);
                }
                else
                {
                    profile = UpdateProfile(profile, gProfile);
                    _context.Profiles.Update(profile);
                }
                await _context.SaveChangesAsync();
                var token = _tokenGenerator.Generate(_authConfig.PrivateKey, gProfile.SourceId, true);
                return Result<(ProfileEntity profile, string token)>.Success((profile, token));
            }
            return Result<(ProfileEntity profile, string token)>.FromFail(tokenResult);
        }
        return Result<(ProfileEntity profile, string token)>.FromFail(tokenResult);
    }

    private ProfileEntity CreateNewOne(GoogleProfile gProfile)
    {
        return new ProfileEntity()
        {
            FirstName = gProfile.FirstName,
            LastName = gProfile.LastName,
            ProfilePicture = gProfile.ProfilePicture,
            GoogleProfileEntity = new GoogleProfileEntity()
            {
                SourceId = gProfile.SourceId,
            }
        };
    }

    private ProfileEntity UpdateProfile(ProfileEntity profile, GoogleProfile gProfile)
    {
        if (!string.Equals(profile.ProfilePicture, gProfile.ProfilePicture))
        {
            profile.ProfilePicture = gProfile.ProfilePicture;
            
        }

        if (string.IsNullOrEmpty(profile.Nickname))
        {
            profile.Nickname = gProfile.FirstName + " " + gProfile.LastName;
        }

        return profile;
    }
}