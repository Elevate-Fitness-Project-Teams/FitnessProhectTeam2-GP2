using Elevate.FitnessCalculation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.FitnessCalculation.Infrastructure.Persistance.Configrations
{
    public class CalculatedMetricsConfigrations : IEntityTypeConfiguration<CalculatedMetrics>
    {
        public void Configure(EntityTypeBuilder<CalculatedMetrics> builder)
        {
            builder.ToTable("CalculatedMetrics");
            builder.HasKey(cm=>cm.Id);
            builder.Property(cm => cm.UserId).IsRequired();
            builder.OwnsOne(cm => cm.Metabolic, m =>
            {
                m.Property(mm => mm.Bmr).HasColumnName("BMR");
                m.Property(mm => mm.Tdee).HasColumnName("TDEE");
                m.Property(mm => mm.CalorieTarget).HasColumnName("CalorieTarget");
            });
            builder.Property(cm => cm.Status)
                    .HasConversion<string>();
                      
        }
    }
}
