using Elevate.Workout.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Workout.Infrastructure.Presistence.Configurations
{
    public sealed class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.ToTable("Exercises");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(e => e.TargetMuscles)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(e => e.Equipment)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(e => e.VideoUrl)
                .HasMaxLength(500)
                .IsRequired(false);
        }
    }
}
