
using FluentValidation;
using Primrose.API.Validators;
using Primrose.Mappers;

namespace Primrose.Validators.Services;

// The actual FluentValidation implementation of an Api validator service
public class FluentValidatorService(IServiceProvider provider) 
    : IValidatorService
{
    private readonly IServiceProvider _provider = provider;

    // This method uses reflection to find a valid service for validating a specific
    // type of request. With LoginRequest mapping to Ivalidator<LoginRequest>.
    //
    // This allows us to simply register a validator service and it will automatically
    // be resolved by this method.
    public ApiResult Validate<T>(T request)
        where T : class
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(request.GetType());
        var validator = _provider.GetService(validatorType) as IValidator;

        var context = new ValidationContext<object>(request);
        var fluentValidationResult = validator?.Validate(context);

        var result = new ApiResult();

        foreach (var error in fluentValidationResult?.Errors ?? [])
        {
            var isValid = Enum.TryParse(error.ErrorCode, out ApiErrorCode code);
            if (!isValid) code = ApiErrorCode.EnumParseFailed;

            var message = ApiErrorCodeMapper.Map(code);

            var validationError = new ApiError(message, error.ErrorCode);
            result.Errors.Add(validationError);
        }

        return result;
    }
}