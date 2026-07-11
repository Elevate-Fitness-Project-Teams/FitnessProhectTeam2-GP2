using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Elevate.Nutrition.Domain.Entities;

namespace Elevate.Nutrition.Infrastructure.Persistence.EntityConfigurations;

public class MealPlanConfiguration : IEntityTypeConfiguration<MealPlan>
{
    public void Configure(EntityTypeBuilder<MealPlan> builder)
    {
        builder.ToTable("MealPlans");

        builder.HasKey(mp => mp.Id);
        builder.Property(mp => mp.Id).ValueGeneratedOnAdd();

        builder.Property(mp => mp.TargetCalorieRangeMin).IsRequired();
        builder.Property(mp => mp.TargetCalorieRangeMax).IsRequired();

        builder.Property(mp => mp.Goal)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(mp => mp.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(mp => mp.Items)
            .WithOne(i => i.MealPlan)
            .HasForeignKey(i => i.MealPlanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
