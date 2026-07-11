using MediatR;

namespace Elevate.Profile.Application.Features.Profiles.UpdatePasswords
{
    public record ChangePasswordCommand(string CurrentPassword,
        string NewPassword,
        string ConfirmPassword) : IRequest;
   
}
