using System.Security.Claims;

namespace Elevate.Nutrition.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst(ClaimTypes.NameIdentifier)
                    ?? user.FindFirst("sub");

        return claim is null
            ? throw new UnauthorizedAccessException("User ID not found in token")
            : Guid.Parse(claim.Value);
    }
}
