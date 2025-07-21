using Microsoft.AspNetCore.Mvc;
using Primrose.Entities.LoginUser;
using Primrose.Entities.RegisterUser;
using Primrose.Services.Authentication;

namespace Primrose.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(IAuthenticationService authService)
    : PrimroseApiController
{
    private readonly IAuthenticationService _authService = authService;

    [HttpPost(nameof(LoginUser))]
    public async Task<IActionResult> LoginUser(LoginUserRequest request)
    {
        return ApiResult(await _authService.LoginUser(request));
    }

    [HttpPost(nameof(RegisterUser))]
    public async Task<IActionResult> RegisterUser(RegisterUserRequest request)
    {
        return ApiResult(await _authService.RegisterUser(request));
    }

    [HttpPost(nameof(DeactivateUser))]
    public async Task<IActionResult> DeactivateUser(RegisterUserRequest request)
    {
        return ApiResult(await _authService.RegisterUser(request));
    }
}