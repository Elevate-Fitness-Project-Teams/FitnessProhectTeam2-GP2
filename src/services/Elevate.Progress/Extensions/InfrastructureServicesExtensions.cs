using Elevate.Progress.Infrastructure.Persistence;
using Elevate.Progress.Integration.Clients;
using Elevate.Progress.Integration.Configuration;
using Elevate.Progress.Integration.Publishers;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Extensions
{
    public static class InfrastructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            // Database
            services.AddDbContext<ProgressDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(
                        configuration.GetConnectionString("DefaultConnection"))));


            // RabbitMQ Configuration
            services.Configure<RabbitMqOptions>(
                configuration.GetSection(
                    RabbitMqOptions.SectionName));


            // RabbitMQ Publisher
            services.AddScoped<
                IRabbitMqPublisher,
                RabbitMqPublisher>();


            // FCE Client
            services.AddHttpClient<IFitnessClient, FitnessClient>(
                client =>
                {
                    client.BaseAddress = new Uri(
                        configuration["Services:FCE"]!);
                });


            return services;
        }
    }
}
