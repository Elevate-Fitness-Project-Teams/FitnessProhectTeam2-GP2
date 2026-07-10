using FluentValidation;

namespace Elevate.Auth.Features.ResetPassword;

public sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.ResetToken)
            .NotEmpty().WithMessage("VAL_REQUIRED_FIELD: resetToken is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("VAL_REQUIRED_FIELD: newPassword is required.")
            .MinimumLength(6).WithMessage("AUTH_WEAK_PASSWORD")
            .Matches(@"[A-Z]").WithMessage("AUTH_WEAK_PASSWORD")
            .Matches(@"\d").WithMessage("AUTH_WEAK_PASSWORD");

        
    }
}
