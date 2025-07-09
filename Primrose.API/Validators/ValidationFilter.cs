using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Primrose.API.Entities;
using Primrose.API.Validators.Services;

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

            if (!result.IsValid)
            {
                if (context.Controller is ControllerBase)
                {
                    context.Result = new BadRequestObjectResult(new ApiResponse()
                    {
                        Success = false,
                        ErrorResult = new ApiValidationResult()
                        {
                            Errors = result.Errors,
                        }
                    });

                    return;
                }
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
