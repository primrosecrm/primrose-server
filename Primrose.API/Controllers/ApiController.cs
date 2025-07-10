using Microsoft.AspNetCore.Mvc;
using Primrose.API.Entities;
using Primrose.API.Validators.Services;

namespace Primrose.API.Controllers;

// This controller currently exists so I dont have to inject the IValidatorService into all controllers
[ApiController]
public class PrimroseApiController(IValidatorService validator)
    : ControllerBase
{
    protected readonly IValidatorService _validator = validator;

    public IActionResult Result<T>(T value)
        where T : ApiResponse
    {
        var isBadRequest = value.ErrorResult.Errors.Count != 0;
        return isBadRequest ? BadRequest(value) : Ok(value);
    }
}