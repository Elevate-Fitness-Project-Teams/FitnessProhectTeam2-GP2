using FluentValidation;

namespace Elevate.Auth.Features.VerifyOtp;

public sealed class VerifyOtpCommandValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("VAL_REQUIRED_FIELD")
            .EmailAddress();

        RuleFor(x => x.Otp)
            .NotEmpty().WithMessage("VAL_REQUIRED_FIELD")
            .Length(6).WithMessage("OTP must be exactly 6 digits.")
            .Matches(@"^\d{6}$").WithMessage("OTP must be numeric.");
    }
}
