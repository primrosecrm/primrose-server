using FluentValidation;
using Primrose.API.Entities.Login;
using Primrose.API.Validators;

namespace Primrose.API.Services.Validators.Authentication;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
                .WithErrorCode(ApiErrorCode.InvalidEmailFormat.ToString())
            .EmailAddress()
                .WithErrorCode(ApiErrorCode.InvalidEmailFormat.ToString());

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithErrorCode(ApiErrorCode.InvalidPasswordFormat.ToString())
            .MinimumLength(8)
                .WithErrorCode(ApiErrorCode.InvalidPasswordFormat.ToString());
    }
}