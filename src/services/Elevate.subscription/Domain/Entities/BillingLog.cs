using Elevate.Subscription.Domain.Enums;

namespace Elevate.subscription.Domain.Entities
{
    public sealed class BillingLog
    {
        private BillingLog() { }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid? SubscriptionId { get; private set; }
        public SubscriptionTier PlanTier { get; private set; }
        public int DurationMonths { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public string? FailureReason { get; private set; }
        public DateTime ProcessedAt { get; private set; }

        public static BillingLog Success(Guid userId, Guid subscriptionId, SubscriptionTier planTier, int durationMonths)
        {
            return new BillingLog
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                SubscriptionId = subscriptionId,
                PlanTier = planTier,
                DurationMonths = durationMonths,
                PaymentStatus = PaymentStatus.Paid,
                FailureReason = null,
                ProcessedAt = DateTime.UtcNow
            };
        }

        public static BillingLog Failure(Guid userId, SubscriptionTier planTier, int durationMonths, string reason)
        {
            return new BillingLog
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                SubscriptionId = null,
                PlanTier = planTier,
                DurationMonths = durationMonths,
                PaymentStatus = PaymentStatus.Failed,
                FailureReason = reason,
                ProcessedAt = DateTime.UtcNow
            };
        }
    }
}
