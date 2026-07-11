namespace Elevate.subscription.Features.GetSubscriptionStatus
{
    public sealed record SubscriptionStatusResponse(string Tier, string Status, DateTime? ExpiresAt);

}
