using Elevate.Subscription.Domain.Enums;
using Elevate.Subscription.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Elevate.subscription.Features.GetSubscriptionStatus
{
    public sealed class GetSubscriptionStatusQueryHandler
        : IRequestHandler<GetSubscriptionStatusQuery, Result<SubscriptionStatusResponse>>
    {
        private readonly SubscriptionDbContext _db;

        public GetSubscriptionStatusQueryHandler(SubscriptionDbContext db) => _db = db;

        public async Task<Result<SubscriptionStatusResponse>> Handle(
            GetSubscriptionStatusQuery request, CancellationToken ct)
        {
            var subscription = await _db.UserSubscriptions
                .AsNoTracking()
                .Where(s => s.UserId == request.UserId)
                .Select(s => new SubscriptionStatusResponse(
                    s.Tier.ToString(),
                    s.Status.ToString(),
                    s.ExpiresAt))
                .FirstOrDefaultAsync(ct);

            if (subscription is null)
            {
                return Result < SubscriptionStatusResponse>.Success(
                    new SubscriptionStatusResponse(SubscriptionTier.Free.ToString(), SubscriptionStatus.Active.ToString(), null));
            }

            return Result<SubscriptionStatusResponse>.Success(subscription);
        }
    }
}
