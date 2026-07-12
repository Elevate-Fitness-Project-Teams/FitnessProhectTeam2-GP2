using Elevate.Profile.Application.Common;
using System.Security.Claims;

namespace Elevate.Profile.Api.Extensions
{
    public static class CurrentUserExtensions
    {
        public static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var user = context.User;
                var currentUser = context.RequestServices.GetRequiredService<ICurrentUserInitializer>();
                var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (currentUser.UserId is null &&
                    Guid.TryParse(id, out var userId))
                {
                    currentUser.UserId = userId;
                }
                currentUser.UserName ??= user.FindFirstValue(ClaimTypes.Name);
                await next();
            });
            return app;
        }
    }
}
