using Microsoft.AspNetCore.Mvc;
using Primrose.API.Models.Login;
using Primrose.API.Services.Authentication;

namespace Primrose.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly AuthenticationService _authService;

    public AuthenticationController(AuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var result = await _authService.LoginUser(request.Email, request.Password);
        return Ok(new LoginResponse(result));
    }

    [HttpPost("Register")]
    public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterUser(request.Email, request.Name, request.Password);
        return Ok(new RegisterResponse(result));
    }
}