using Elevate.Profile.Application.Common;
using Elevate.Profile.Domain.Entities;
using Elevate.Profile.Domain.Exceptions;
using Elevate.Profile.Domain.Interfaces;
using Elevate.Profile.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Profile.Application.Features.Profiles.Commands
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand>
    {
        private readonly IGeneralRepository<UserProfile> _repository;
        private readonly ICurrentUser currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProfileCommandHandler(IGeneralRepository<UserProfile> repository,ICurrentUser currentUser,IUnitOfWork unitOfWork)
        {
            _repository = repository;
            this.currentUser = currentUser;
           _unitOfWork = unitOfWork;
        }
        //public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        //{


        //}
        public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUser.UserId
                ?? throw new UnauthorizedAccessException();

            var profile = await _repository.GetById(userId);

            if (profile is null)
                throw new NotFoundException($"Profile with id {userId} not found.");

            var emailExists = await _repository.GetAll()
                .AnyAsync(x =>
                    x.Email.ToString() == request.email &&
                    x.UserId != userId,
                    cancellationToken);

            if (emailExists)
                throw new ConflictException("AUTH_EMAIL_EXISTS.");

            var fullName = new FullName(request.firstName, request.lastName);
            var email = new Email(request.email);

            profile.UpdateProfile(fullName, email, request.phoneNumber);

            await _unitOfWork.ExecuteAsync(() =>
            {
                _repository.Update(profile);
                return Task.CompletedTask;
            });
        }
    }
}
