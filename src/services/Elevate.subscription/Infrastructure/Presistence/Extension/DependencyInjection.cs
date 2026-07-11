using Elevate.subscription.Infrastructure.Services;
using Elevate.Subscription.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Interfaces;

namespace Elevate.subscription.Infrastructure.Presistence.Extension
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSubscriptionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SubscriptionDbContext).Assembly));
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddDbContext<SubscriptionDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
