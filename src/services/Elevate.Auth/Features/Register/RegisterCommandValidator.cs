using FluentValidation;

namespace Elevate.Auth.Features.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    // Egyptian mobile: starts with 010/011/012/015, exactly 11 digits
    private const string EgyptianPhoneRegex = @"^(010|011|012|015)\d{8}$";

    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("VAL_REQUIRED_FIELD")
            .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("VAL_REQUIRED_FIELD")
            .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("VAL_REQUIRED_FIELD")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("VAL_REQUIRED_FIELD")
            .MinimumLength(6).WithMessage("AUTH_WEAK_PASSWORD")
            .Matches(@"[A-Z]").WithMessage("AUTH_WEAK_PASSWORD")
            .Matches(@"\d").WithMessage("AUTH_WEAK_PASSWORD");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("VAL_REQUIRED_FIELD")
            .Matches(EgyptianPhoneRegex).WithMessage("Phone number must be a valid Egyptian mobile number.");
    }
}
