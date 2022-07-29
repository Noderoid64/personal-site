using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Services.Auth.Models;

public class AuthInfo
{
    public string Token { get; }
    public string RefreshToken { get; set; }
    public string? ProfilePicture { get; }
    public string NickName { get; }

    public AuthInfo(ProfileEntity profileEntity, string token, string refreshToken)
    {
        Token = token;
        ProfilePicture = profileEntity.ProfilePicture;
        NickName = profileEntity.Nickname;
        RefreshToken = refreshToken;
    }
}