using Microsoft.AspNetCore.Mvc;
using Primrose.API.Entities.Login;
using Primrose.API.Services.Authentication;
using Primrose.API.Validators.Services;

namespace Primrose.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController
    : PrimroseApiController
{
    private readonly AuthenticationService _authService;

    public AuthenticationController(AuthenticationService authService, IValidatorService validator)
        : base(validator)
    {
        _authService = authService;
    }

    [HttpPost(nameof(Login))]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return LoginResponse.Ok(await _authService.LoginUser(request));
    }

    [HttpPost(nameof(Register))]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        return RegisterResponse.Ok(await _authService.RegisterUser(request));
    }
}