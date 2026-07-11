
using Elevate.subscription.Infrastructure.Common.Interfaces;
using Elevate.subscription.Infrastructure.Presistence.Extension;
using Elevate.subscription.Infrastructure.Services;
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
            builder.Services.AddScoped<IBillingSimulator, BillingSimulator>();
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
