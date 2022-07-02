namespace PersonalSite.Services.Auth.Models;

public class AuthConfig
{
    public readonly string PrivateKey;
    public readonly string Issuer;
    public readonly string Audience;
    
    public AuthConfig(IConfiguration config)
    {
        var section = config.GetSection("AuthConfig");
        PrivateKey = section["PrivateKey"];  
        Issuer = section["Issuer"];  
        Audience = section["Audience"];  
    }
}