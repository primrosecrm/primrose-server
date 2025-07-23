using Microsoft.AspNetCore.Mvc;
using Primrose.Entities;

namespace Primrose.Controllers;

// This controller currently exists so I dont have to inject the IValidatorService into all controllers
[ApiController]
[RequireHttps]
public class PrimroseApiController()
    : ControllerBase
{
    public ActionResult ApiResult<T>(T value)
        where T : ApiResponse 
    {
        var hasErrors = value.ErrorResult.Errors.Count != 0;
        return hasErrors ? BadRequest(value) : Ok(value);
    }
}