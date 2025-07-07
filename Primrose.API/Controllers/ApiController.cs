using Microsoft.AspNetCore.Mvc;
using Primrose.API.Validators.Services;

namespace Primrose.API.Controllers;

// This controller currently exists so I dont have to inject the IValidatorService into all controllers
[ApiController]
public class PrimroseApiController : ControllerBase
{
    protected readonly IValidatorService _validator;

    public PrimroseApiController(IValidatorService validator)
    {
        _validator = validator;
    }

    public async Task<ActionResult> Handle<TRequest, TResponse>(TRequest request, Func<Task<TResponse>> action)
        where TRequest : class
    {
        var validation = _validator.Validate(request);
        if (!validation.IsValid)
        { 
            var failureResponse = Activator.CreateInstance(typeof(TResponse), false, validation);
            return BadRequest(failureResponse);
        }

        var response = await action();
        return Ok(response);
    }
}