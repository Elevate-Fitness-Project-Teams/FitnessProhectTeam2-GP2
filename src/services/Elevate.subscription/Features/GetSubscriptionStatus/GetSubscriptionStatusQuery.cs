using MediatR;
using SharedKernel;

namespace Elevate.subscription.Features.GetSubscriptionStatus
{
    public sealed record GetSubscriptionStatusQuery(Guid UserId) 
        : IRequest<Result<SubscriptionStatusResponse>>;

}
