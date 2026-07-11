using MediatR;

namespace Elevate.Profile.Application.Features.Profiles.UploadPhoto
{
    public record UploadPhotoCommand(IFormFile ProfilePicture) : IRequest;
   
}
