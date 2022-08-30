using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Services.Auth;

namespace PersonalSite.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/auth")]
public class AuthController : ApiController
{
    private readonly AuthFacade _authFacade;

    public AuthController(AuthFacade authFacade)
    {
        _authFacade = authFacade;
    }

    #if DEBUG
    [AllowAnonymous]
    [HttpGet("admin")]
    public async Task<IActionResult> LoginAsAdmin()
    {
        return await Auth("noderoid64@gmail.com", "qwertyui");
    }
    #endif

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(string email, string password, string nickname)
    {
        var result = await _authFacade.RegisterAsync(email, password, nickname);
        return BuildResponse(result);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Auth(string email, string pass)
    {
        var result = await _authFacade.AuthorizeAsync(email, pass);
        if (result.IsSuccess)
            return Ok(result.Value);
        return Unauthorized();
    }
    
    [AllowAnonymous]
    [HttpPost("google")]
    public async Task<IActionResult> AuthByGoogleCode(string code)
    {
        var result = await _authFacade.AuthorizeByGoogleAsync(code);
        return BuildResponse(result);
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(string refreshToken)
    {
        var userId = GetUderIdForRefresh();
        var result = await _authFacade.RefreshToken(refreshToken.Replace(' ', '+').Trim(), userId);
        return BuildResponse(result);
    }
}