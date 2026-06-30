using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuthService.Infrastructure.Persistence.Seeders;


public static class AuthDbSeeder
{
    public static async Task SeedAsync(AuthDbContext db, ILogger logger)
    {
        try
        {
            var pending = (await db.Database.GetPendingMigrationsAsync()).ToList();

            if (pending.Count != 0)
            {
                logger.LogInformation("[AuthService] Applying {Count} pending migration(s)...", pending.Count);
                await db.Database.MigrateAsync();
                logger.LogInformation("[AuthService] Migrations applied successfully.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "[AuthService] Database seeding failed.");
            throw;
        }
    }
}