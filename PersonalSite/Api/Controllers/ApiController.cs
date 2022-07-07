using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace PersonalSite.Api.Controllers;

public class ApiController : ControllerBase
{
    protected string GetUserId()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        return identity.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;       
    }

    protected string GetUserType()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        return identity.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.AuthenticationMethod))?.Value;       
    }
}