using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PersonalSite.Services.Auth.Models;
using PersonalSite.Services.Auth.Services;
using SimpleInjector;

namespace PersonalSite.Services.Auth;

public static class AuthExtension
{
    public static void AddAuth(this IServiceCollection sc, IConfiguration config)
    {
        var authConfig = new AuthConfig(config);
        sc.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            var Key = Encoding.UTF8.GetBytes(authConfig.PrivateKey);
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = authConfig.Issuer,
                ValidAudience = authConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Key)
            };
        });
    }

    public static void RegisterAuth(this Container container, IConfiguration config)
    {
        container.Register(() => new AuthConfig(config));
        container.Register<AuthFacade>(Lifestyle.Scoped);
        container.Register<GoogleApi>(Lifestyle.Scoped);
        container.Register<TokenGenerator>(Lifestyle.Scoped);
    }
}