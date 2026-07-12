using Elevate.Profile.Application.Common;
using Elevate.Profile.Domain.Entities;
using Elevate.Profile.Domain.Interfaces;
using Elevate.subscription.Domain.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Profile.Infrastructure.Consumers
{
    public class SubscriptionUpgradedConsumer : IConsumer<SubscriptionUpgradedEvent>
    {
        private readonly IGeneralRepository<UserProfile> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionUpgradedConsumer(IGeneralRepository<UserProfile> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;

        }

        public async Task Consume(ConsumeContext<SubscriptionUpgradedEvent> context)
        {
            var message = context.Message;

            var profile = await _repository.GetById(message.UserId);

            if (profile is not null)
            {
                profile.UpdatePremiumStatus(true);

                await _unitOfWork.SaveChangesAsync(context.CancellationToken);
            }
        }
    }
}
