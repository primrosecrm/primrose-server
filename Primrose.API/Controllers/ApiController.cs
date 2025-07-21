using Microsoft.AspNetCore.Mvc;
using Primrose.Entities;

namespace Primrose.Controllers;

// This controller currently exists so I dont have to inject the IValidatorService into all controllers
[ApiController]
public class PrimroseApiController()
    : ControllerBase
{
    public IActionResult ApiResult<T>(T value)
        where T : ApiResponse
    {
        var isBadRequest = value.ErrorResult.Errors.Count != 0;
        return isBadRequest ? BadRequest(value) : Ok(value);
    }
}