namespace PersonalSite.Services.Auth.Models;

public class AuthConfig
{
    public readonly string PrivateKey;
    public readonly string Issuer;
    public readonly string Audience;
    public readonly string GoogleClientId;
    public readonly string GoogleSecret;
    public readonly string GoogleRedirectURI;
    public readonly int RefreshTokenHoursValidity;
    
    public AuthConfig(IConfiguration config)
    {
        var section = config.GetSection("AuthConfig");
        PrivateKey = section["PrivateKey"];  
        Issuer = section["Issuer"];  
        Audience = section["Audience"];
        RefreshTokenHoursValidity = int.Parse(section["RefreshTokenHoursValidity"]);
        GoogleClientId = section["Google:ClientId"];
        GoogleSecret = section["Google:ClientSecret"];
        GoogleRedirectURI = section["Google:RedirectURI"];
    }
}