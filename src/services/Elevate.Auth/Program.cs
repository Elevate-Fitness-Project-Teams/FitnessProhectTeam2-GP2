using AuthService.Infrastructure.Persistence;
using AuthService.Infrastructure.Persistence.Seeders;
using Elevate.Auth.Domain.Interfaces;
using Elevate.Auth.Features.Login;
using Elevate.Auth.Infrastructure;
using Elevate.Auth.Infrastructure.Identity;
using Elevate.Auth.Infrastructure.Presistence.Interfaces;
using Elevate.Auth.Infrastructure.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using System.Text;

namespace Elevate.Auth
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(connectionString));


            var jwtSecret = builder.Configuration["JwtSettings:Secret"];
            if (string.IsNullOrEmpty(jwtSecret))
            {
                throw new InvalidOperationException("JWT Secret is missing from Configuration!");
            }

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
                };
            });

            builder.Services.AddAuthServices();
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            var rabbitMqHost = builder.Configuration["MessageBroker:Host"] ?? "localhost";

            builder.Services.AddMassTransit(x =>
            {
                x.AddEntityFrameworkOutbox<AuthDbContext>(o =>
                {
                    o.UseSqlServer();
                    o.UseBusOutbox(); 
                });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqHost, "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    var dbContext = services.GetRequiredService<AuthDbContext>();
                    logger.LogInformation("[AuthService] Starting database migration & seeding...");
                    await AuthDbSeeder.SeedAsync(dbContext, logger);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "[AuthService] An error occurred while migrating or seeding the database.");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapAuthEndpoints();
            app.Run();
        }
    }
}