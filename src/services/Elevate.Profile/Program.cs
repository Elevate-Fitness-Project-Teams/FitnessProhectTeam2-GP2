
using Elevate.Profile.Api.Extensions;
using Elevate.Profile.Application.Common;
using Elevate.Profile.Application.Features.Profile.Queries;
using Elevate.Profile.Domain.Interfaces;
using Elevate.Profile.Infrastructure;
using Elevate.Profile.Infrastructure.Authentication;
using Elevate.Profile.Infrastructure.Repositories;
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

            builder.Services.AddScoped<CurrentUser>();
            builder.Services.AddScoped<ICurrentUserInitializer, CurrentUser>(x => x.GetRequiredService<CurrentUser>());
            builder.Services.AddScoped<ICurrentUser, CurrentUser>(x => x.GetRequiredService<CurrentUser>());

            builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetProfileQuery).Assembly);
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
