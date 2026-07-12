using Elevate.Auth.Domain.DomainEvents;
using Elevate.Profile.Domain.Entities;
using Elevate.Profile.Domain.Interfaces;
using Elevate.Profile.Domain.ValueObjects;
using MassTransit;


namespace Elevate.Profile.Infrastructure.Consumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegisteredEvent>
    {
        private readonly IGeneralRepository<UserProfile> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UserRegisteredConsumer(
            IGeneralRepository<UserProfile> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            var message = context.Message;

            var fullName =new FullName(message.FirstName, message.LastName);
            var email = new Email(message.Email);

            var newProfile = UserProfile.Create(
                message.UserId,
                fullName,
                email,
                message.PhoneNumber,
                null

                );

            await _unitOfWork.ExecuteAsync(async () =>
            {
                _repository.Add(newProfile);
                await _unitOfWork.SaveChangesAsync(context.CancellationToken);
            });
        }
    }
}
