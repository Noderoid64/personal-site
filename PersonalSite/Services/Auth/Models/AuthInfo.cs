using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Services.Auth.Models;

public class AuthInfo
{
    public string Token { get; }
    public string? ProfilePicture { get; }
    public string NickName { get; }

    public AuthInfo(ProfileEntity profileEntity, string token)
    {
        Token = token;
        ProfilePicture = profileEntity.ProfilePicture;
        NickName = profileEntity.Nickname;
    }
}