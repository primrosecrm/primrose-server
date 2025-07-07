
using FluentValidation;

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
    {
        var fluentValidation = _provider.GetRequiredService<IValidator<T>>();
        var fluentValidationResult = fluentValidation.Validate(request);

        var result = new ApiValidationResult();

        foreach (var e in fluentValidationResult.Errors)
            result.Errors.Add(new ApiValidationError(e.PropertyName, e.ErrorMessage));

        return result;
    }
}