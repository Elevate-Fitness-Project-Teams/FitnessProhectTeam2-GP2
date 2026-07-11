using FluentValidation;

namespace Elevate.Auth.Features.ForgotPassword;

public sealed class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("VAL_REQUIRED_FIELD")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
