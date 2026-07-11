using Elevate.Workout.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace Elevate.Workout.Infrastructure.Persistence;

public static class MigrationExtensions
{
    public static void ApplyWorkoutMigrations(this IApplicationBuilder app)
    {
        // Create a temporary scope to resolve scoped services like DbContext safely on startup
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using var context = scope.ServiceProvider.GetRequiredService<WorkOutDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<WorkOutDbContext>>();

        try
        {
            logger.LogInformation("Starting automated database migration for Workout Module...");

            context.Database.Migrate();

            logger.LogInformation("Workout database migrations applied successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the Workout database.");
            throw; 
        }
    }
}