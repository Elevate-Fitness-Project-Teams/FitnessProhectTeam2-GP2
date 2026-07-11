using FluentValidation;

namespace Elevate.subscription.Features.GetSubscriptionStatus
{
    public sealed class GetSubscriptionStatusQueryValidator : AbstractValidator<GetSubscriptionStatusQuery>
    {
        public GetSubscriptionStatusQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
