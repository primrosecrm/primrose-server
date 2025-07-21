namespace Primrose.Validators.Services;

// allows us to be less coupled to specific validation services, such as FluentValidation
public interface IValidatorService
{
    public ApiResult Validate<T>(T request)
        where T : class;
}