using Elevate.Profile.Application.Common;
using Elevate.Profile.Application.Features.Profiles.UploadPhoto;
using Elevate.Profile.Domain.Entities;
using Elevate.Profile.Domain.Exceptions;
using Elevate.Profile.Domain.Interfaces;
using MediatR;

namespace Elevate.Profile.Application.Features.Profiles.UpdatePhoto
{
    public class UploadPhotoCommandHandler : IRequestHandler<UploadPhotoCommand>
    {
        private readonly IGeneralRepository<UserProfile> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public UploadPhotoCommandHandler(
            IGeneralRepository<UserProfile> repository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var profile = await _repository.GetById(userId.Value);

            if (profile == null)
            {
                throw new NotFoundException($"User profile with ID {userId} not found.");
            }

            if (request.ProfilePicture == null || request.ProfilePicture.Length == 0)
            {
                throw new BadRequestException("Profile picture is required.");
            }

            var extension = Path.GetExtension(request.ProfilePicture.FileName).ToLowerInvariant();

            if (extension != ".jpg" &&
                extension != ".jpeg" &&
                extension != ".png")
            {
                throw new BadRequestException("Invalid file type. Only JPG, JPEG, and PNG are allowed.");
            }

            if (request.ProfilePicture.Length > 5 * 1024 * 1024)
            {
                throw new BadRequestException("File size exceeds the limit of 5MB.");
            }

            try
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.ProfilePicture.CopyToAsync(stream, cancellationToken);
                }

                // Update profile picture path
                profile.UpdateProfilePicture(fileName);

                // Save to database
                await _unitOfWork.ExecuteAsync(async () =>
                {
                     _repository.Update(profile);
                });
            }
            catch (Exception ex)
            {
                throw new FileUploadException($"SRV_FILE_UPLOAD_FAILED: {ex.Message}");
            }
        }
    }
}