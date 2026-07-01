using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Elevate.Nutrition.Domain.Interfaces;
using Elevate.Nutrition.Infrastructure.Persistence;
using Elevate.Nutrition.Infrastructure.Persistence.Repositories;

namespace Elevate.Nutrition.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration config)
    {
        var connectionString = config.GetConnectionString("NutritionDb");

        services.AddDbContext<NutritionDbContext>(options =>
            options.UseMySql(connectionString,
                ServerVersion.AutoDetect(connectionString),
                mysqlOptions => mysqlOptions.EnableStringComparisonTranslations()));

        services.AddScoped<IMealRepository, MealRepository>();
        services.AddScoped<IMealPlanRepository, MealPlanRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
