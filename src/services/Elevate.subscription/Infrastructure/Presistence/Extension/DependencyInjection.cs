using Elevate.Subscription.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Elevate.subscription.Infrastructure.Presistence.Extension
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSubscriptionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SubscriptionDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
