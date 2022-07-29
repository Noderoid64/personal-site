using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace PersonalSite.Api.Controllers;

public class ApiController : ControllerBase
{
    protected int GetUserId()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        return int.Parse(identity.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value);       
    }

    protected int GetUderIdForRefresh()
    {
        string authHeaderValue = HttpContext.Request.Headers.FirstOrDefault(x => x.Key.Equals("Authorization")).Value;
        var token = authHeaderValue.Substring(7);
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var userId = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
        return int.Parse(userId);
    }
}