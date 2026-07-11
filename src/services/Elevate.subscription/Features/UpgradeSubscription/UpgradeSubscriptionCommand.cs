using MediatR;
using SharedKernel;

namespace Elevate.subscription.Features.UpgradeSubscription
{
    public sealed record UpgradeSubscriptionCommand(
       Guid UserId, string PlanTier, int DurationMonths) 
        : IRequest<Result<UpgradeSubscriptionResponse>>;
}
