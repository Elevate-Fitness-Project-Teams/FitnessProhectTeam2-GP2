using SharedKernel.Interfaces;
using System.Security.Claims;

namespace Elevate.subscription.Infrastructure.Services
{
    public sealed class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public Guid UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
            }
        }

        public bool IsAuthenticated
            => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    }
}
