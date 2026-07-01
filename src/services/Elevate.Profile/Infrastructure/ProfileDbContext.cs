using Elevate.Profile.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Profile.Infrastructure
{
    public class ProfileDbContext:DbContext
    {
        public ProfileDbContext(DbContextOptions<ProfileDbContext> options): base(options)
        {
            
        }
        public DbSet<UserProfile> UsersProfile { get; set; }
        public DbSet<NotificationSettings> NotificationSettings { get; set; }
        public DbSet<PrivacySettings> PrivacySettings { get; set; }
        public DbSet<UserPreferences> UserPreferences { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfileDbContext).Assembly);
        }
    }
}
