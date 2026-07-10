using Elevate.Workout.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Workout.Infrastructure.Presistence.Configurations
{
    public class WorkoutExerciseCatalogItemConfiguration : IEntityTypeConfiguration<WorkoutExerciseCatalogItem>
    {
        public void Configure(EntityTypeBuilder<WorkoutExerciseCatalogItem> builder)
        {
            builder.ToTable("WorkoutExerciseCatalogItems");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.WorkoutId).IsRequired();
            builder.Property(c => c.ExerciseId).IsRequired();
            builder.Property(c => c.OrderIndex).IsRequired();

            builder.HasIndex(c => new { c.WorkoutId, c.OrderIndex });

            builder.HasOne(c => c.Exercise)
                .WithMany()
                .HasForeignKey(c => c.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Domain.Entities.Workout>()
                .WithMany(w => w.Exercises)
                .HasForeignKey(c => c.WorkoutId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
