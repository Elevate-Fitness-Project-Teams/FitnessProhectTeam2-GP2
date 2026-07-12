using Elevate.Auth.Domain.DomainEvents;
using Elevate.subscription.Domain.Entities;
using Elevate.Subscription.Infrastructure.Persistence;
using MassTransit;

namespace Elevate.subscription.Infrastructure.Consumers
{
    public sealed class UserRegisteredConsumer : IConsumer<UserRegisteredEvent>
    {
        private readonly SubscriptionDbContext _dbContext;

        public UserRegisteredConsumer(SubscriptionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            var message = context.Message;

            var newSubscription = UserSubscription.CreateFreeTrial(message.UserId, trialDays: 14);
                

            await _dbContext.UserSubscriptions.AddAsync(newSubscription, context.CancellationToken);
            await _dbContext.SaveChangesAsync(context.CancellationToken);
        }
    }
}
