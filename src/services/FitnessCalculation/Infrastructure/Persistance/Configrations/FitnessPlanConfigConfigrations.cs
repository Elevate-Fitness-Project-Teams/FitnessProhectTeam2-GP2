using Elevate.FitnessCalculation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.FitnessCalculation.Infrastructure.Persistance.Configrations
{
    public class FitnessPlanConfigConfigrations : IEntityTypeConfiguration<FitnessPlanConfig>
    {
        public void Configure(EntityTypeBuilder<FitnessPlanConfig> builder)
        {
            builder.ToTable("FitnessPlanConfigs");
            builder.HasKey(f=> f.PlanId);
            builder.Property(f => f.Goal)
                .HasConversion<string>();
            builder.Property(f => f.status)
                .HasConversion<string>();
            builder.OwnsOne(f => f.caloriesRange, cr =>
            {
                cr.Property(c => c.MinCalorie).HasColumnName("MinCalories");
                cr.Property(c => c.MaxCalorie).HasColumnName("MaxCalories");
            });
            builder.OwnsOne(x => x.planConfigration, owned =>
            {
                owned.Property(x => x.EstimatedDuration);
                owned.Property(x => x.WorkOutsperWeek);
                owned.Property(x => x.ProgramType);
            });

        }
    }
}
