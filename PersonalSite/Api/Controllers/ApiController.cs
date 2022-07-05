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
}