using Elevate.Progress.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Infrastructure.Persistence
{
    public class ProgressDbContext : DbContext
    {
        public ProgressDbContext(DbContextOptions<ProgressDbContext> options)
            : base(options)
        {
        }

        public DbSet<WorkoutSession> WorkoutSessions => Set<WorkoutSession>();

        public DbSet<WorkoutLog> WorkoutLogs => Set<WorkoutLog>();

        public DbSet<WorkoutLogExercises> WorkoutLogExercises => Set<WorkoutLogExercises>();

        public DbSet<WeightHistory> WeightHistory => Set<WeightHistory>();

        public DbSet<Streak> Streaks => Set<Streak>();

        public DbSet<UserStatistics> UserStatistics => Set<UserStatistics>();

        public DbSet<Achievement> Achievements => Set<Achievement>();

        public DbSet<UserAchievement> UserAchievements => Set<UserAchievement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProgressDbContext).Assembly);
        }
        
    }
}