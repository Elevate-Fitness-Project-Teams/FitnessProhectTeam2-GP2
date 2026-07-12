using Elevate.Profile.Domain.Entities;
using Elevate.Profile.Domain.Interfaces;
using Elevate.subscription.Domain.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Profile.Infrastructure.Consumers
{
    public class SubscriptionCancelledConsumer : IConsumer<SubscriptionCancelledEvent>
    {
        private readonly IGeneralRepository<UserProfile> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionCancelledConsumer(
            IGeneralRepository<UserProfile> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<SubscriptionCancelledEvent> context)
        {
            var message = context.Message;

            var profile = await _repository
                .GetById(message.UserId);

            if (profile is not null)
            {
                profile.UpdatePremiumStatus(false);

                await _unitOfWork.ExecuteAsync(async () =>
                {
                    _repository.Update(profile);
                    await _unitOfWork.SaveChangesAsync(context.CancellationToken);
                });
            }
        }
    }
}
