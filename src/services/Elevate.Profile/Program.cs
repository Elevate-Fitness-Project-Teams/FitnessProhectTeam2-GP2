
using Elevate.Profile.Api.Extensions;
using Elevate.Profile.Application.Common;
using Elevate.Profile.Domain.Interfaces;
using Elevate.Profile.Infrastructure;
using Elevate.Profile.Infrastructure.Authentication;
using Elevate.Profile.Infrastructure.Consumers;
using Elevate.Profile.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Extension.DependencyInjection;

namespace Elevate.Profile
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


            builder.Services.AddDbContext<ProfileDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>)); // تأكد من اسم الـ Implementation عندك
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<CurrentUser>();
            builder.Services.AddScoped<ICurrentUserInitializer, CurrentUser>(x => x.GetRequiredService<CurrentUser>());
            builder.Services.AddScoped<ICurrentUser, CurrentUser>(x => x.GetRequiredService<CurrentUser>());
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<SubscriptionUpgradedConsumer>();
                x.AddConsumer<SubscriptionCancelledConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCurrentUser();
            app.MapControllers();
            app.MapEndpoints();
            app.Run();
        }
    }
}
