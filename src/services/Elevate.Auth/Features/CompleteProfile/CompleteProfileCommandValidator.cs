using FluentValidation;

namespace Elevate.Auth.Features.CompleteProfile;

public sealed class CompleteProfileCommandValidator : AbstractValidator<CompleteProfileCommand>
{
    public CompleteProfileCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("VAL_REQUIRED_FIELD: userId is required.");
    }
}
