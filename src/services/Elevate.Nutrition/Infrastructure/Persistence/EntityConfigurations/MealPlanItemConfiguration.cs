using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Elevate.Nutrition.Domain.Entities;

namespace Elevate.Nutrition.Infrastructure.Persistence.EntityConfigurations;

public class MealPlanItemConfiguration : IEntityTypeConfiguration<MealPlanItem>
{
    public void Configure(EntityTypeBuilder<MealPlanItem> builder)
    {
        builder.ToTable("MealPlanItems");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedOnAdd();

        builder.Property(i => i.ServingCount)
            .IsRequired()
            .HasDefaultValue(1);

        builder.HasOne(i => i.Meal)
            .WithMany()
            .HasForeignKey(i => i.MealId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
