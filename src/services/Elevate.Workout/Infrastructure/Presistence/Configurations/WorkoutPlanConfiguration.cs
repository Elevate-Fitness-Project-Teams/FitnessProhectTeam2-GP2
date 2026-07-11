using Elevate.Workout.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Workout.Infrastructure.Presistence.Configurations
{
    public sealed class WorkoutPlanConfiguration : IEntityTypeConfiguration<WorkoutPlan>
    {
        public void Configure(EntityTypeBuilder<WorkoutPlan> builder)
        {
            builder.ToTable("WorkoutPlans");

            builder.HasKey(wp => wp.Id);

            builder.Property(wp => wp.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(wp => wp.Description)
                .HasMaxLength(1000) 
                .IsRequired();

            builder.Property(wp => wp.Difficulty)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(wp => wp.DurationWeeks)
                .IsRequired();

            builder.HasMany(wp => wp.Workouts)
                .WithOne(wp=> wp.WorkoutPlan) 
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}
