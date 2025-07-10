using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Primrose.API.Entities;
using Primrose.API.Entities.Login;
using Primrose.API.Validators.Services;

namespace Primrose.API.Validators;

public class ValidationFilter : IActionFilter
{
    private readonly IValidatorService _validator;

    public ValidationFilter(IValidatorService validator)
    {
        _validator = validator;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null) continue;

            var result = _validator.Validate(argument);

            if (result.Errors.Count is 0) continue;
            if (context.Controller is not ControllerBase) continue;

            var errorResponse = new BadResponse()
            {
                Success = false,
                ErrorResult = new ApiResult()
                {
                    Errors = result.Errors,
                }
            };

            context.Result = new BadRequestObjectResult(errorResponse);
            return;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}