using Elevate.Profile.Application.Common;
using Elevate.Profile.Domain.Entities;
using Elevate.Profile.Domain.Exceptions;
using Elevate.Profile.Domain.Interfaces;
using Elevate.Profile.Domain.ValueObjects;
using MediatR;

namespace Elevate.Profile.Application.Features.Profiles.Commands
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand>
    {
        private readonly IGeneralRepository<UserProfile> _repository;
        private readonly ICurrentUser _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProfileCommandHandler(
            IGeneralRepository<UserProfile> repository,
            ICurrentUser currentUser,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            UpdateProfileCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            var profile = await _repository.GetById(userId!.Value);

            if (profile == null)
                throw new NotFoundException($"Profile with id {userId} not found.");

            var emailExists = _repository.GetAll()
                .Any(x => x.Email.ToString() == request.email &&
                          x.UserId != userId.Value);

            if (emailExists)
                throw new ConflictException("AUTH_EMAIL_EXISTS.");

            FullName fullName = new(request.firstName, request.lastName);
            Email Email = new(request.email);

            profile.UpdateProfile(fullName, Email, request.phoneNumber);

            await _unitOfWork.ExecuteAsync(async () =>
            {
                _repository.Update(profile);
            });
        }
    }
}