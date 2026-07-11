using Elevate.Workout.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Workout.Infrastructure.Persistence.Configurations;

public sealed class WorkoutSessionConfiguration : IEntityTypeConfiguration<WorkoutSession>
{
    public void Configure(EntityTypeBuilder<WorkoutSession> builder)
    {
        builder.ToTable("WorkoutSessions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(x => x.Exercises)
            .WithOne()
            .HasForeignKey(x => x.WorkoutSessionId)
            .OnDelete(DeleteBehavior.Cascade); 

        // Explicitly back the read-only collection with the private list field
        builder.Metadata.FindNavigation(nameof(WorkoutSession.Exercises))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}