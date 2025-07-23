using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Primrose.Entities;
using Primrose.Validators.Services;

namespace Primrose.Validators.Filters;

public class ValidationFilter(IValidatorService validator) : IActionFilter
{
    private readonly IValidatorService _validator = validator;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null) continue;

            var result = _validator.Validate((ApiRequest)argument);

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