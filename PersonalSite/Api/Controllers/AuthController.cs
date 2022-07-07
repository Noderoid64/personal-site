﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Services.Auth;

namespace PersonalSite.Api.Controllers;

[Authorize]
[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthFacade _authFacade;
    private readonly IMapper _mapper;

    public AuthController(AuthFacade authFacade, IMapper mapper)
    {
        _authFacade = authFacade;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(string email, string password, string nickname)
    {
        var profile = await _authFacade.RegisterAsync(email, password, nickname);
        if (profile.IsSuccess)
            return Ok();
        return BadRequest(profile.ErrorMessage);
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
        var res = await _authFacade.AuthorizeByGoogleAsync(code);
        if (res.IsSuccess)
            return Ok(res.Value);
        return BadRequest(res.ErrorMessage);
    }
}