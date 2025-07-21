using Primrose.Entities.DeactivateUser;
using Primrose.Entities.LoginUser;
using Primrose.Entities.RegisterUser;
using Primrose.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Primrose.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthenticationController(IAuthenticationService authService)
    : PrimroseApiController
{
    private readonly IAuthenticationService _authService = authService;

    [HttpPost("Login")]
    public async Task<ActionResult<LoginUserResponse>> LoginUser(LoginUserRequest request)
    {
        return ApiResult(await _authService.LoginUser(request));
    }

    [HttpPost("Register")]
    public async Task<ActionResult<RegisterUserResponse>> RegisterUser(RegisterUserRequest request)
    {
        return ApiResult(await _authService.RegisterUser(request));
    }

    [HttpPost("Deactivate")]
    public async Task<ActionResult<DeactivateUserResponse>> DeactivateUser(DeactivateUserRequest request)
    {
        return ApiResult(await _authService.DeactivateUser(request));
    }
}