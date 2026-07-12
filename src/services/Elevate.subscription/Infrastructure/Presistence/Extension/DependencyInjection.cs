using Elevate.subscription.Infrastructure.Common.Interfaces;
using Elevate.subscription.Infrastructure.Services;
using Elevate.Subscription.Infrastructure.Persistence;
using MassTransit;
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
            services.AddScoped<IBillingSimulator, BillingSimulator>();
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                });
            });
            return services;
        }
    }
}
