using Elevate.Workout.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Workout.Infrastructure.Persistence.Configurations;

public sealed class WorkoutSessionConfiguration : IEntityTypeConfiguration<WorkoutSession>
{
    public void Configure(EntityTypeBuilder<WorkoutSession> builder)
    {
        builder.ToTable("WorkoutSessions");

        builder.HasKey(s => s.Id); // Guid

        builder.HasMany(s => s.Exercises)
               .WithOne()
               .HasForeignKey(e => e.WorkoutSessionId) 
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(s => s.Type)
       .HasConversion<string>();

        builder.Metadata.FindNavigation(nameof(WorkoutSession.Exercises))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}