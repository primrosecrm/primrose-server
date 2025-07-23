using Primrose.Entities.DeactivateUser;
using Primrose.Entities.LoginUser;
using Primrose.Entities.RegisterUser;
using Primrose.Services.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Primrose.Controllers;

[ApiController]
[ApiVersion("1.0")]
[RequireHttps]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController(IAuthService authService)
    : PrimroseApiController
{
    private readonly IAuthService _authService = authService;

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult<LoginUserResponse>> LoginUser(LoginUserRequest request)
    {
        return ApiResult(await _authService.LoginUser(request));
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<ActionResult<RegisterUserResponse>> RegisterUser(RegisterUserRequest request)
    {
        return ApiResult(await _authService.RegisterUser(request));
    }
}