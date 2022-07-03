using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Api.Dtos;
using PersonalSite.Services.Auth;

namespace PersonalSite.Api.Controllers;

[Authorize]
[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(AuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(string email, string password, string nickname)
    {
        var profile = await _authService.RegisterAsync(email, password, nickname);
        if (profile.IsSuccess)
            return Ok();
        return BadRequest(profile.ErrorMessage);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Auth(string email, string pass)
    {
        var result = await _authService.AuthorizeAsync(email, pass);
        if (!result.IsSuccess)
            return Unauthorized();
        
        var profileDto = _mapper.Map<ProfileDto>(result.Value.profile);
        profileDto.Token = result.Value.token;
        return Ok(profileDto);
    }
    
    [AllowAnonymous]
    [HttpPost("google")]
    public async Task<IActionResult> AuthByGoogleCode(string code)
    {
        var res = await _authService.AuthorizeByGoogleAsync(code);
        if (res.IsSuccess)
        {
            var profileDto = _mapper.Map<ProfileDto>(res.Value.profile);
            profileDto.Token = res.Value.token;
            return Ok(profileDto);
        }
            
        return BadRequest(res.ErrorMessage);
    }
}