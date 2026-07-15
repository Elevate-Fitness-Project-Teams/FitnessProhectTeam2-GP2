using Elevate.Progress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Progress.Infrastructure.Persistence.Configuration
{
    public class StreakConfiguration : IEntityTypeConfiguration<Streak>
    {
        public void Configure(EntityTypeBuilder<Streak> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Streak_CurrentStreak",
                    "CurrentStreak >= 0");

                t.HasCheckConstraint(
                    "CK_Streak_LongestStreak",
                    "LongestStreak >= 0");
            });
        }
    }
}
