using Elevate.Progress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Progress.Infrastructure.Persistence.Configuration
{
    public class WeightHistoryConfiguration : IEntityTypeConfiguration<WeightHistory>
    {
        public void Configure(EntityTypeBuilder<WeightHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Notes)
                .HasMaxLength(500);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_WeightHistory_Weight",
                    "Weight >= 40 AND Weight <= 200");
            });
        }
    }
}
