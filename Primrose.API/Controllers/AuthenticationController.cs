using Microsoft.AspNetCore.Mvc;
using Primrose.API.Entities.Login;
using Primrose.API.Entities.RegisterUser;
using Primrose.API.Services.Authentication;
using Primrose.API.Validators.Services;

namespace Primrose.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(IAuthenticationService authService, IValidatorService validator)
    : PrimroseApiController(validator)
{
    private readonly IAuthenticationService _authService = authService;

    [HttpPost(nameof(Login))]
    public async Task<IActionResult> Login(LoginUserRequest request)
    {
        return Result(await _authService.LoginUser(request));
    }

    [HttpPost(nameof(Register))]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        return Result(await _authService.RegisterUser(request));
    }
}