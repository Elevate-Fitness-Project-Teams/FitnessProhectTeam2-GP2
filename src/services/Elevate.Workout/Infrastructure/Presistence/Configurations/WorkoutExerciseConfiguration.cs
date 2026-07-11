using Elevate.Workout.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Workout.Infrastructure.Presistence.Configurations
{
    public sealed class WorkoutExerciseConfiguration : IEntityTypeConfiguration<WorkoutExercise>
    {
        public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
        {
            builder.ToTable("WorkoutExercises");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ExerciseName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(x => x.Sets)
                .WithOne()
                .HasForeignKey(x => x.WorkoutExerciseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(WorkoutExercise.Sets))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
