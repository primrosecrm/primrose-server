namespace Primrose.API.Validators.Services;

// allows us to be less coupled to specific validation services, such as FluentValidation
public interface IValidatorService
{
    public ApiValidationResult Validate<T>(T request)
        where T : class;
}