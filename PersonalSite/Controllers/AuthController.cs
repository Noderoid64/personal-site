using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Services.Auth;

namespace PersonalSite.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Auth(string email, string pass)
    {
        var result = _authService.Authorize(email, pass);
        if (string.IsNullOrEmpty(result))
        {
            return Unauthorized();
        }
        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpGet("google")]
    public async Task<IActionResult> AuthByGoogleCode(string code)
    {
        // var a = new HttpClient();
        // var request = new HttpRequestMessage();
        // request.Headers
        //     .Accept
        //     .Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        // request.Method = HttpMethod.Post;
        // request.RequestUri = new Uri("https://oauth2.googleapis.com/token?code=" + code + "&client_id=714351833041-ohb8v036d4efaoo6g0bs38us42ff4v8r.apps.googleusercontent.com&client_secret=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&redirect_uri=http://localhost:4200&grant_type=authorization_code");
        //
        // var response = await a.SendAsync(request);
        // Console.WriteLine(response);
        //
        // return Ok();
        throw new NotImplementedException();
    }
}