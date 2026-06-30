using Elevate.Auth.Infrastructure.Identity;

namespace Elevate.Auth.Domain.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUser user, IList<string> roles);
    }
}
