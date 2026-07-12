using MediatR;
using SharedKernel;

namespace Elevate.subscription.Features.CancelSubscription
{
    public sealed record CancelSubscriptionCommand(Guid UserId,CancellationToken CancellationToken) : IRequest<Result<CancelSubscriptionResponse>>;

}
