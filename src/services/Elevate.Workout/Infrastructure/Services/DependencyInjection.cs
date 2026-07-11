using Elevate.Workout.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Workout.Infrastructure.Services
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddWorkoutInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WorkOutDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
