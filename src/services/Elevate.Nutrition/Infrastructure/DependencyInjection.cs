using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Elevate.Nutrition.Application.Interfaces;
using Elevate.Nutrition.Domain.Interfaces;
using Elevate.Nutrition.Infrastructure.Persistence;
using Elevate.Nutrition.Infrastructure.Persistence.Repositories;
using Elevate.Nutrition.Infrastructure.Services;

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

        var fceBaseUrl = config["Services:FceApi:BaseUrl"];

        if (string.IsNullOrWhiteSpace(fceBaseUrl))
        {
            services.AddScoped<IFceIntegrationService, NullFceIntegrationService>();
        }
        else
        {
            services.AddHttpClient<IFceIntegrationService, FceIntegrationService>(client =>
            {
                client.BaseAddress = new Uri(fceBaseUrl);
                client.Timeout = TimeSpan.FromSeconds(5);
            })
            .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(
                3, retryAttempt => TimeSpan.FromMilliseconds(200 * retryAttempt)));
        }

        return services;
    }
}
