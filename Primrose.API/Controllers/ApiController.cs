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
}