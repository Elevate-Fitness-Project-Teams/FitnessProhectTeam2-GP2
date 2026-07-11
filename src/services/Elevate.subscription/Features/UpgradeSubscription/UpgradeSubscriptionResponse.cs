namespace Elevate.subscription.Features.UpgradeSubscription
{
    public sealed record UpgradeSubscriptionResponse(string ActiveTier, DateTime ExpiresAt);

}
