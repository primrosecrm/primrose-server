using Microsoft.AspNetCore.Mvc;
using Primrose.API.Entities.Login;
using Primrose.API.Entities.Register;
using Primrose.API.Validators.Services;

namespace Primrose.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController
    : PrimroseApiController
{
    private readonly IAuthenticationService _authService;

    public AuthenticationController(IAuthenticationService authService, IValidatorService validator)
        : base(validator)
    {
        _authService = authService;
    }

    [HttpPost(nameof(Login))]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return Ok(await _authService.LoginUser(request));
    }

    [HttpPost(nameof(Register))]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        return Ok(await _authService.RegisterUser(request));
    }
}