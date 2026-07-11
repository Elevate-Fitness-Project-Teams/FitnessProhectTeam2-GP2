using SharedKernel;

namespace Elevate.subscription.Domain.Erros
{
    public static class SubscriptionErrors
    {
        public static readonly Error BillingFailed = new(
            "Subscription.BillingFailed",
            "Billing authorization failed.",
            ErrorType.Failure);

        public static readonly Error NoActiveSubscription = new(
            "Subscription.NoActiveSubscription",
            "No active subscription to cancel.",
            ErrorType.NotFound);
    }
}
