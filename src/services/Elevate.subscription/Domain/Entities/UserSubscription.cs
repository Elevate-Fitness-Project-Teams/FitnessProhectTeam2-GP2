using Elevate.subscription.Domain.Events;
using Elevate.Subscription.Domain.Enums;
using SharedKernel;

namespace Elevate.subscription.Domain.Entities
{
    public sealed class UserSubscription : BaseEntity
    {
        private UserSubscription() { }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public SubscriptionTier Tier { get; private set; }
        public SubscriptionStatus Status { get; private set; }
        public DateTime? ExpiresAt { get; private set; }
        public bool AutoRenew { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }


        public static UserSubscription CreatePremium(Guid userId, int durationMonths)
        {
            var now = DateTime.UtcNow;

            var subscription = new UserSubscription
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Tier = SubscriptionTier.Premium,
                Status = SubscriptionStatus.Active,
                ExpiresAt = now.AddMonths(durationMonths),
                AutoRenew = true,
                CreatedAt = now
            };

            subscription.AddDomainEvent(new SubscriptionUpgradedEvent(
                subscription.Id,
                subscription.UserId,
                subscription.Tier,
                subscription.ExpiresAt.Value,
                DateTime.UtcNow));

            return subscription;
        }


        public void UpgradeToPremium(int durationMonths)
        {
            Tier = SubscriptionTier.Premium;
            Status = SubscriptionStatus.Active;
            ExpiresAt = DateTime.UtcNow.AddMonths(durationMonths);
            AutoRenew = true;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new SubscriptionUpgradedEvent(
                Id, UserId, Tier, ExpiresAt.Value, DateTime.UtcNow));
        }


        public void Cancel()
        {
            if (Status != SubscriptionStatus.Active)
                throw new InvalidOperationException("No active subscription to cancel.");

            AutoRenew = false;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new SubscriptionCancelledEvent(
                Id, UserId, ExpiresAt, DateTime.UtcNow));
        }


        public void MarkExpiredIfPastDue()
        {
            if (ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value && Status == SubscriptionStatus.Active)
            {
                Status = SubscriptionStatus.Expired;
                UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
