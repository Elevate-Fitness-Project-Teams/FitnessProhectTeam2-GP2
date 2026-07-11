using MediatR;

namespace Elevate.Profile.Application.Features.Profiles.UpdateSettings
{
    public record updateSettingsCommand(string? theme,bool? workoutReminders): IRequest;
    
}
