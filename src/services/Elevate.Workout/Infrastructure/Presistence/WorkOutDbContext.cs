using Elevate.Workout.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Workout.Infrastructure.Presistence
{
    public class WorkOutDbContext :DbContext
    {
        public WorkOutDbContext(DbContextOptions<WorkOutDbContext> options) : base(options)
        {
        }
        public DbSet<Domain.Entities.Workout> Workouts => Set<Domain.Entities.Workout>();
        public DbSet<WorkoutSession> WorkoutSessions => Set<WorkoutSession>();
        public DbSet<WorkoutPlan> WorkoutPlans => Set<WorkoutPlan>();
        public DbSet<WorkoutExercise> WorkoutExercises => Set<WorkoutExercise>();
        public DbSet<ExerciseSet> ExerciseSets => Set<ExerciseSet>();
        public DbSet<Exercise> Exercises => Set<Exercise>();
        public DbSet<WorkoutExerciseCatalogItem> WorkoutExerciseCatalogItems => Set<WorkoutExerciseCatalogItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkOutDbContext).Assembly);
        }

        
    }
}
