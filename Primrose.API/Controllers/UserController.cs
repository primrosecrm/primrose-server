using Primrose.Entities.DeactivateUser;
using Primrose.Entities.LoginUser;
using Primrose.Entities.RegisterUser;
using Primrose.Services.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Primrose.Services.User;

namespace Primrose.Controllers;

[ApiController]
[ApiVersion("1.0")]
[RequireHttps]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController(IUserService userService)
    : PrimroseApiController
{
    private readonly IUserService _userService = userService;

    [HttpPost("Deactivate")]
    public async Task<ActionResult<DeactivateUserResponse>> DeactivateUser(DeactivateUserRequest request)
    {
        return ApiResult(await _userService.DeactivateUser(request));
    }

    [HttpPost("Activate")]
    public async Task<ActionResult<ActivateUserResponse>> ActivateUser(ActivateUserRequest request)
    {
        return ApiResult(await _userService.ActivateUser(request));
    }
}