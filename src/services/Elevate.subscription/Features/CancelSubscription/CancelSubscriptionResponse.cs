namespace Elevate.subscription.Features.CancelSubscription
{
    public sealed record CancelSubscriptionResponse
        (bool AutoRenewCancelled, DateTime? EffectiveAt);

}
