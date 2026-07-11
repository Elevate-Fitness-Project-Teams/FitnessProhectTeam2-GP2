using Elevate.Profile.Application.Common;
using Elevate.Profile.Domain.Entities;
using Elevate.Profile.Domain.Enums;
using Elevate.Profile.Domain.Exceptions;
using Elevate.Profile.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Profile.Application.Features.Profiles.UpdateSettings
{
    public class UpdateSettingsCommandHandler : IRequestHandler<updateSettingsCommand>
    {
        private readonly IGeneralRepository<UserProfile> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public UpdateSettingsCommandHandler(
            IGeneralRepository<UserProfile> repository,
            ICurrentUser currentUser,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(updateSettingsCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            var profile = await _repository
                .GetAll()
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (profile == null)
                throw new NotFoundException("Profile Not Found");

            if (request.theme != null) profile.Preferences.Theme = Enum.Parse<Theme>(request.theme.ToLower(), true);

            if (request.workoutReminders.HasValue)
            {
                profile.NotificationSettings.WorkoutReminders = request.workoutReminders.Value;
            }

            profile.UpdatePreferences(profile.Preferences);
            profile.UpdateNotificationSettings(profile.NotificationSettings);

            await _unitOfWork.ExecuteAsync(() =>
            {
                _repository.Update(profile);
                return Task.CompletedTask;
            });
        }
    }
}