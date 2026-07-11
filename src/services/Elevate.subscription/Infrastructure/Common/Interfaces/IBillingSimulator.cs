using Elevate.Subscription.Domain.Enums;

namespace Elevate.subscription.Infrastructure.Common.Interfaces
{
    public interface IBillingSimulator
    {
        Task<bool> AuthorizeAsync(Guid userId, SubscriptionTier tier, int durationMonths, CancellationToken ct);
    }
}
