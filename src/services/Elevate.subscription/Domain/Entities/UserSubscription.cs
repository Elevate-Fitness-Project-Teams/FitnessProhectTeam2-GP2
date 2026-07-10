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
        public Guid SubscriptionPlanId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public SubscriptionStatus Status { get; private set; }

        public static UserSubscription Create(Guid userId, Guid planId, int durationInDays)
        {
            var now = DateTime.UtcNow;
            return new UserSubscription
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                SubscriptionPlanId = planId,
                StartDate = now,
                EndDate = now.AddDays(durationInDays),
                Status = SubscriptionStatus.Active
            };
        }

        public void CheckExpiration()
        {
            if (DateTime.UtcNow > EndDate && Status == SubscriptionStatus.Active)
            {
                Status = SubscriptionStatus.Expired;
            }
        }

        public void CancelSubscription()
        {
            if (Status == SubscriptionStatus.Active)
            {
                Status = SubscriptionStatus.Cancelled;
            }
        }
        public void UpgradePlan(Guid newPlanId, int newDurationInDays)
        {
            SubscriptionPlanId = newPlanId;
            EndDate = DateTime.UtcNow.AddDays(newDurationInDays);
            Status = SubscriptionStatus.Active;

            AddDomainEvent(new SubscriptionUpgradedEvent(Id, UserId, newPlanId, DateTime.UtcNow));
        }
    }
}
