using FluentValidation;
using Primrose.API.Entities.RegisterUser;
using Primrose.API.Validators;

namespace Primrose.API.Services.Validators.Authentication;

public class RegisterRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
                .WithErrorCode(ApiErrorCode.InvalidEmailFormat.ToString());

        RuleFor(x => x.Password)
            .MinimumLength(8)
                .WithErrorCode(ApiErrorCode.InvalidPasswordFormat.ToString());

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithErrorCode(ApiErrorCode.InvalidNameFormat.ToString())
            .MinimumLength(3)
                .WithErrorCode(ApiErrorCode.InvalidNameFormat.ToString());
    }
}