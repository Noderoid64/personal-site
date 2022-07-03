using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PersonalSite.Core.Entities;
using PersonalSite.Infrastructure.Common.Models;
using PersonalSite.Infrastructure.EF;
using PersonalSite.Services.Auth.Models;

namespace PersonalSite.Services.Auth;

public class AuthService
{
    private readonly AuthConfig _authConfig;
    private readonly ApplicationContext _context;

    public AuthService(AuthConfig authConfig, ApplicationContext context)
    {
        _authConfig = authConfig;
        _context = context;
    }

    public async Task<Result<ProfileEntity?>> RegisterAsync(string email, string password, string nickname)
    {
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
        return Result<ProfileEntity?>.Success(profile);
    }
    
    public async Task<Result<string?>> AuthorizeAsync(string email, string password)
    {
        var profile = await _context.ProfileCredentials
            .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        if (profile == null)
            return Result<string>.Fail($"Login failed: try different credentials");
        // Else we generate JSON Web Token
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_authConfig.PrivateKey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "TestName")                    
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Result<string?>.Success(tokenHandler.WriteToken(token));
    }
}