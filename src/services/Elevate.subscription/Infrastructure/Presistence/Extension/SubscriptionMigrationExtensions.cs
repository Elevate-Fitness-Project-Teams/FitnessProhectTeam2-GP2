using Elevate.Subscription.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Elevate.subscription.Infrastructure.Presistence.Extension
{
    public static class SubscriptionMigrationExtensions
    {
        public static void ApplySubscriptionMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            using var context = scope.ServiceProvider.GetRequiredService<SubscriptionDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SubscriptionDbContext>>();

            try
            {
                logger.LogInformation("Checking and applying pending database migrations for Subscription Module...");

                context.Database.Migrate();

                logger.LogInformation("Subscription database migrations applied successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while executing pending migrations for Subscription database.");
                throw; 
            }
        }
    }
}
