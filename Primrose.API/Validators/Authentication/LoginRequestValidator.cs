using FluentValidation;
using Primrose.API.Validators;
using Primrose.Entities.LoginUser;

namespace Primrose.Validators.Authentication;

public class LoginRequestValidator : AbstractValidator<LoginUserRequest>
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