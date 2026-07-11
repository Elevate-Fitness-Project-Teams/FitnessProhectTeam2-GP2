using Elevate.Workout.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Workout.Infrastructure.Presistence.Configurations
{
    public sealed class ExerciseSetConfiguration : IEntityTypeConfiguration<ExerciseSet>
    {
        public void Configure(EntityTypeBuilder<ExerciseSet> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();

            builder.Property(e => e.Reps).IsRequired();
            builder.Property(e => e.WeightInKg).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.SetNumber).IsRequired();
            builder.Property(e => e.IsCompleted).IsRequired();
        }
    }
}
