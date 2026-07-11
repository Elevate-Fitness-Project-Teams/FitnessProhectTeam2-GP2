using Elevate.Profile.Domain.ValueObjects;
using MediatR;

namespace Elevate.Profile.Application.Features.Profiles.Commands
{
    public record UpdateProfileCommand(
        string firstName,
        string lastName,
        string email,
        string phoneNumber):IRequest;
  
}
