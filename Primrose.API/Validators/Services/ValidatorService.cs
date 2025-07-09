
using FluentValidation;
using Microsoft.AspNetCore.Identity.Data;

namespace Primrose.API.Validators.Services;

// The actual FluentValidation implementation of an Api validator service
public class FluentValidatorService : IValidatorService
{
    private readonly IServiceProvider _provider;

    public FluentValidatorService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public ApiValidationResult Validate<T>(T request)
        where T : class
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(request.GetType());
        var validator = _provider.GetService(validatorType) as IValidator;

        // var fluentValidation = _provider.GetRequiredService<IValidator<T>>();
        var context = new ValidationContext<object>(request);
        var fluentValidationResult = validator.Validate(context);

        var result = new ApiValidationResult();

        foreach (var error in fluentValidationResult.Errors)
        {
            var validationError = new ApiValidationError(error.PropertyName, error.ErrorMessage);
            result.Errors.Add(validationError);
        }

        return result;
    }
}