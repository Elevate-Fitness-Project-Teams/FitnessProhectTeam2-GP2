using Elevate.Progress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class WorkoutLogConfiguration : IEntityTypeConfiguration<WorkoutLog>
{
    public void Configure(EntityTypeBuilder<WorkoutLog> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Difficulty)
            .HasConversion<int>();

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_WorkoutLog_Rating",
                "Rating >= 1 AND Rating <= 5");

            t.HasCheckConstraint(
                "CK_WorkoutLog_Duration",
                "Duration >= 0");

            t.HasCheckConstraint(
                "CK_WorkoutLog_CaloriesBurned",
                "CaloriesBurned >= 0");
        });
    }
}