using Elevate.Workout.Features.Workouts.BrowseWorkoutPlans;
using Elevate.Workout.Features.Workouts.GetAllWorkouts;
using Elevate.Workout.Features.Workouts.GetWorkoutDetails;
using Elevate.Workout.Features.Workouts.GetWorkoutPlanDetail;
using Elevate.Workout.Features.Workouts.GetWorkoutsByCategory;
using Elevate.Workout.Features.Workouts.GetWorkoutsByPlan;
using Elevate.Workout.Features.Workouts.StartWorkoutSession;
using Elevate.Workout.Infrastructure.Persistence;
using Elevate.Workout.Infrastructure.Presistence;
using Elevate.Workout.Infrastructure.Presistence.Seed;
using Elevate.Workout.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Elevate.Workout
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddWorkoutInfrastructure(builder.Configuration);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            var jwtSecret = builder.Configuration["JwtSettings:Secret"];
            if (string.IsNullOrEmpty(jwtSecret))
            {
                throw new InvalidOperationException("JWT Secret is missing from Workout Configuration!");
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
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"] ?? "ElevateWorkoutService",
                    ValidAudience = builder.Configuration["JwtSettings:Audience"] ?? "ElevateFitnessApp",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
                };
            });

            builder.Services.AddAuthorization();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.ApplyWorkoutMigrations();
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<WorkOutDbContext>();
                    await WorkoutDbContextSeeder.SeedAsync(db);
                }
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapWorkoutEndpoints();          
            app.MapGetWorkoutDetailsEndpoint(); 
            app.MapGetWorkoutsByPlanEndpoint(); 
            app.MapGetWorkoutsByCategoryEndpoint();
            app.MapStartWorkoutSessionEndpoint();
            app.MapBrowseWorkoutPlansEndpoint();
            app.MapGetWorkoutPlanDetailEndpoint();
            app.MapControllers();

            app.Run();
        }
    }
}
