using Elevate.Workout.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Workout.Infrastructure.Presistence
{
    public class WorkOutDbContext : DbContext
    {
        public WorkOutDbContext(DbContextOptions<WorkOutDbContext> options) : base(options)
        {
        }

        public DbSet<WorkoutSession> WorkoutSessions => Set<WorkoutSession>();
        public DbSet<WorkoutExercise> WorkoutExercises => Set<WorkoutExercise>();
        public DbSet<ExerciseSet> ExerciseSets => Set<ExerciseSet>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkOutDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Place for Auditing or Domain Events Dispatcher invocation before committing transactions
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
