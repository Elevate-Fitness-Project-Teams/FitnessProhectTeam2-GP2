using FluentValidation;

namespace Elevate.Auth.Features.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("VAL_REQUIRED_FIELD")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("VAL_REQUIRED_FIELD");
    }
}