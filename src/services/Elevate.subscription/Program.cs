using Elevate.subscription.Infrastructure.Common.Interfaces;
using Elevate.subscription.Infrastructure.Consumers;
using Elevate.subscription.Infrastructure.Presistence.Extension;
using Elevate.subscription.Infrastructure.Services;
using Elevate.Subscription.Infrastructure.Persistence;
using MassTransit;
using MassTransit.RabbitMqTransport;
using SharedKernel.Extension.DependencyInjection;

namespace Elevate.subscription
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSubscriptionInfrastructure(builder.Configuration);
            builder.Services.AddEndpoints(typeof(Program).Assembly);

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<UserRegisteredConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    // 2. السطر ده هو اللي هيكريت الـ Queue بتاعة الـ Subscription ويربطها أوتوماتيك
                    cfg.ConfigureEndpoints(context);
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.ApplySubscriptionMigrations();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}
