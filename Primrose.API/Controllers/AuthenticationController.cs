using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
        var validation = _validator.Validate(request);
        if (!validation.IsValid) return LoginResponse.Bad(validation);

        var result = await _authService.LoginUser(request);
        return LoginResponse.Ok(result);
    }

    [HttpPost(nameof(Register))]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var validation = _validator.Validate(request);
        if (!validation.IsValid) return RegisterResponse.Bad(validation);

        var result = await _authService.RegisterUser(request);
        return RegisterResponse.Ok(result);
    }
}