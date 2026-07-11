using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Elevate.Nutrition.Domain.Entities;

namespace Elevate.Nutrition.Infrastructure.Persistence.EntityConfigurations;

public class MealConfiguration : IEntityTypeConfiguration<Meal>
{
    public void Configure(EntityTypeBuilder<Meal> builder)
    {
        builder.ToTable("Meals");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).ValueGeneratedOnAdd();

        builder.Property(m => m.Name).IsRequired().HasMaxLength(200);
        builder.Property(m => m.NutritionFacts).HasMaxLength(2000);
        builder.Property(m => m.Ingredients).IsRequired().HasMaxLength(4000);
        builder.Property(m => m.Instructions).HasMaxLength(4000);
        builder.Property(m => m.Calories).IsRequired();
        builder.Property(m => m.ProteinGrams).IsRequired();

        builder.Property(m => m.MealType)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property("_tagsCsv")
            .HasColumnName("Tags")
            .HasMaxLength(500);

        builder.HasIndex(m => m.MealType);
        builder.HasIndex(m => m.Calories);
    }
}
