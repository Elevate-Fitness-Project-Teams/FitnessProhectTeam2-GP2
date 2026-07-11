using Elevate.Subscription.Domain.Enums;
using FluentValidation;

namespace Elevate.subscription.Features.UpgradeSubscription
{
    public sealed class UpgradeSubscriptionCommandValidator : AbstractValidator<UpgradeSubscriptionCommand>
    {
        public UpgradeSubscriptionCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.PlanTier)
                .NotEmpty()
                .Must(tier => Enum.TryParse<SubscriptionTier>(tier, true, out var parsed) && parsed == SubscriptionTier.Premium)
                .WithMessage("planTier must be 'Premium'.");

            RuleFor(x => x.DurationMonths)
                .InclusiveBetween(1, 24);
        }
    }
}
