using Elevate.subscription.Domain.Erros;
using Elevate.subscription.Domain.Events;
using Elevate.Subscription.Domain.Enums;
using Elevate.Subscription.Infrastructure.Persistence;
using MassTransit;
using MassTransit.Transports;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Elevate.subscription.Features.CancelSubscription
{
    public sealed class CancelSubscriptionCommandHandler
        : IRequestHandler<CancelSubscriptionCommand, Result<CancelSubscriptionResponse>>
    {
        private readonly SubscriptionDbContext _db;
        private readonly IPublishEndpoint _publishEndpoint;

        public CancelSubscriptionCommandHandler(SubscriptionDbContext db,IPublishEndpoint publishEndpoint)
        {
            _db = db;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result<CancelSubscriptionResponse>> Handle(
            CancelSubscriptionCommand request, CancellationToken ct)
        {
            var subscription = await _db.UserSubscriptions
                .FirstOrDefaultAsync(s => s.UserId == request.UserId && s.Status == SubscriptionStatus.Active, ct);

            if (subscription is null)
                return Result.Failure<CancelSubscriptionResponse>(SubscriptionErrors.NoActiveSubscription);

            //use factory static methon in Supscription entity to cancel
            subscription.Cancel();

            await _publishEndpoint.Publish(new SubscriptionCancelledEvent(
            SubscriptionId: subscription.Id,
            UserId: subscription.UserId,
            ExpiresAt: subscription.ExpiresAt,
            OccurredAt: DateTime.UtcNow));

            // save Event in outbox table and update rows in subscription table in same transaction
            await _db.SaveChangesAsync(ct);

            return Result.Success(new CancelSubscriptionResponse(true, subscription.ExpiresAt));
        }
    }
}
