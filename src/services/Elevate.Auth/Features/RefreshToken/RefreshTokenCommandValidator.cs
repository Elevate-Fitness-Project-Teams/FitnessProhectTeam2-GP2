using FluentValidation;

namespace Elevate.Auth.Features.RefreshToken;

public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.ExpiredAccessToken)
            .NotEmpty().WithMessage("VAL_REQUIRED_FIELD: expiredAccessToken is required.");

        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("VAL_REQUIRED_FIELD: refreshToken is required.");
    }
}
