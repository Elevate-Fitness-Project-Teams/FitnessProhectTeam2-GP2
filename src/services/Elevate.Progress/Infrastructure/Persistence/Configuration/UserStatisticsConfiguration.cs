using Elevate.Progress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Progress.Infrastructure.Persistence.Configuration
{
    public class UserStatisticsConfiguration : IEntityTypeConfiguration<UserStatistics>
    {
        public void Configure(EntityTypeBuilder<UserStatistics> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_UserStatistics_TotalWorkouts",
                    "TotalWorkouts >= 0");

                t.HasCheckConstraint(
                    "CK_UserStatistics_TotalCaloriesBurned",
                    "TotalCaloriesBurned >= 0");

                t.HasCheckConstraint(
                    "CK_UserStatistics_TotalWeightLost",
                    "TotalWeightLost >= 0");

                t.HasCheckConstraint(
                    "CK_UserStatistics_CurrentStreak",
                    "CurrentStreak >= 0");

                t.HasCheckConstraint(
                    "CK_UserStatistics_LongestStreak",
                    "LongestStreak >= 0");

                t.HasCheckConstraint(
                    "CK_UserStatistics_TotalWorkoutDuration",
                    "TotalWorkoutDuration >= 0");
            });
        }
    }
}
