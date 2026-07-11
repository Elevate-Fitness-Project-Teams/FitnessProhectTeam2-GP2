using Elevate.subscription.Domain.Erros;
using Elevate.Subscription.Domain.Enums;
using Elevate.Subscription.Infrastructure.Persistence;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Elevate.subscription.Features.CancelSubscription
{
    public sealed class CancelSubscriptionCommandHandler
        : IRequestHandler<CancelSubscriptionCommand, Result<CancelSubscriptionResponse>>
    {
        private readonly SubscriptionDbContext _db;

        public CancelSubscriptionCommandHandler(SubscriptionDbContext db) => _db = db;

        public async Task<Result<CancelSubscriptionResponse>> Handle(
            CancelSubscriptionCommand request, CancellationToken ct)
        {
            var subscription = await _db.UserSubscriptions
                .FirstOrDefaultAsync(s => s.UserId == request.UserId && s.Status == SubscriptionStatus.Active, ct);

            if (subscription is null)
                return Result.Failure<CancelSubscriptionResponse>(SubscriptionErrors.NoActiveSubscription);

            subscription.Cancel();
            await _db.SaveChangesAsync(ct);

            return Result.Success(new CancelSubscriptionResponse(true, subscription.ExpiresAt));
        }
    }
}
