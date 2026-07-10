using Elevate.Profile.Application.Common;
using Elevate.Profile.Application.Features.Profile.DTO;
using Elevate.Profile.Application.Features.Profile.Queries;
using Elevate.Profile.Domain.Entities;
using Elevate.Profile.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Profile.Application.Features.Profiles.Queries
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, GetProfileDTo>
    {
        private readonly IGeneralRepository<UserProfile> _repository;
        private readonly ICurrentUser _currentUser;
        public GetProfileQueryHandler(IGeneralRepository<UserProfile> repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }
        public async Task<GetProfileDTo> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {

            var userId = _currentUser.UserId;
            var profile = await _repository.GetAll()
                                .FirstOrDefaultAsync(x => x.UserId == userId);
            if (profile == null)
            {
                throw new Exception("Profile not found");
            }
            return new GetProfileDTo
            {
                UserId = profile.UserId,
                Name = profile.Name.FirstName + " " + profile.Name.LastName,
                Email = profile.Email.ToString() ?? " ",
                PhoneNumber = profile.PhoneNumber,
                ProfilePictureUrl = profile.ProfilePictureUrl,
                IsPremiumCached = profile.IsPremiumCached,
                MemberSince = profile.MemberSince,
            };
        }
    }
}
