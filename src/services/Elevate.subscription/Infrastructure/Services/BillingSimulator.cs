using Elevate.subscription.Infrastructure.Common.Interfaces;
using Elevate.Subscription.Domain.Enums;

namespace Elevate.subscription.Infrastructure.Services
{
    public sealed class BillingSimulator : IBillingSimulator
    {
        public Task<bool> AuthorizeAsync(Guid userId, SubscriptionTier tier, int durationMonths, CancellationToken ct)
        {
            // for a real payment provider later without touching the handler.
            bool isSuccess = Random.Shared.Next(0, 100) < 80;
            return Task.FromResult(isSuccess);
        }
    }
}
