using Elevate.Workout.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Workout.Infrastructure.Presistence
{
    public class WorkOutDbContext : BaseDbContext
    {
        public WorkOutDbContext(DbContextOptions<WorkOutDbContext> options, IMediator mediator) : base(options, mediator)
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

        
    }
}
