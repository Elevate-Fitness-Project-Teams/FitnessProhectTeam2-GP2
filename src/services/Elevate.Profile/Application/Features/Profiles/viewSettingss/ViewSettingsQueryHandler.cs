using Elevate.Profile.Application.Common;
using Elevate.Profile.Application.Features.Profiles.DTO;
using Elevate.Profile.Domain.Entities;
using Elevate.Profile.Domain.Exceptions;
using Elevate.Profile.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Profile.Application.Features.Profiles.viewSettingss
{
    public class ViewSettingsQueryHandler : IRequestHandler<ViewSettingsQuery, ViewSettingsDto>
    {
        private readonly IGeneralRepository<UserProfile> _repository;
        private readonly ICurrentUser _currentUser;

        public ViewSettingsQueryHandler(IGeneralRepository<UserProfile> repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }
        public async Task<ViewSettingsDto> Handle(ViewSettingsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            var profileDTO = await _repository.GetAll()
                                .Where(x => x.UserId == userId)
                                .Select(x=>new ViewSettingsDto
                                {
                                    Preferences = new UserPreferencesDto
                                    {
                                        Language = x.Preferences.Language.ToString(),
                                        Theme = x.Preferences.Theme.ToString()
                                    },
                                    Notifications = new NotificationSettingsDto
                                    {
                                        EmailNotifications = x.NotificationSettings.EmailNotifications,
                                        PushNotifications = x.NotificationSettings.PushNotifications
                                    },
                                    Privacy = new PrivacySettingsDto
                                    {
                                        ProfileVisibility = x.PrivacySettings.ProfileVisibility.ToString(),
                                        ShareProgress = x.PrivacySettings.ShowProgressToFriends
                                    }
                                }

                                )
                                .FirstOrDefaultAsync(cancellationToken);

            if (profileDTO == null)
            {
                throw new NotFoundException("Profile not found");
            }

           

            return profileDTO;
        }
    }
}
