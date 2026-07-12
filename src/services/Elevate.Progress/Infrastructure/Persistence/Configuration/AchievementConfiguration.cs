using Elevate.Progress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Progress.Infrastructure.Persistence.Configuration
{
    public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
    {
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.IconUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Type)
                .HasConversion<int>();

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Achievement_RequiredValue",
                    "RequiredValue >= 0");
            });
        }
    }
}
