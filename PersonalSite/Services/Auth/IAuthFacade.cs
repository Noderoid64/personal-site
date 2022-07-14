using PersonalSite.Core.Models.Entities;
using PersonalSite.Infrastructure.Common.Models;
using PersonalSite.Services.Auth.Models;

namespace PersonalSite.Services.Auth;

public interface IAuthFacade
{
    Task<Result<ProfileEntity>> RegisterAsync(string email, string password, string nickname);
    Task<Result<AuthInfo>> AuthorizeAsync(string email, string password);
    Task<Result<AuthInfo>> AuthorizeByGoogleAsync(string authCode);
}