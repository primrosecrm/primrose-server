using Microsoft.AspNetCore.Mvc;
using Primrose.API.Entities.Login;
using Primrose.API.Services.Authentication;
using Primrose.API.Validators.Services;

namespace Primrose.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly AuthenticationService _authService;
    private readonly IValidatorService _validator;

    public AuthenticationController(AuthenticationService authService, IValidatorService validator)
    {
        _authService = authService;
        _validator = validator;
    }

    [HttpPost]
    [Route(nameof(Login))]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var validation = _validator.Validate(request);
        if (!validation.IsValid) return BadRequest(new LoginResponse(false, validation));

        var result = await _authService.LoginUser(request.Email, request.Password);
        return Ok(new LoginResponse(result));
    }

    [HttpPost]
    [Route(nameof(Register))]
    public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request)
    {
        var validation = _validator.Validate(request);
        if (!validation.IsValid) return BadRequest(new LoginResponse(false, validation));

        var result = await _authService.RegisterUser(request.Email, request.Name, request.Password);
        return Ok(new RegisterResponse(result));
    }
}