using Microsoft.EntityFrameworkCore;
using Elevate.Nutrition.Domain.Entities;
using Elevate.Nutrition.Infrastructure.Persistence.EntityConfigurations;

namespace Elevate.Nutrition.Infrastructure.Persistence;

public class NutritionDbContext : DbContext
{
    public DbSet<Meal> Meals => Set<Meal>();
    public DbSet<MealPlan> MealPlans => Set<MealPlan>();
    public DbSet<MealPlanItem> MealPlanItems => Set<MealPlanItem>();

    public NutritionDbContext(DbContextOptions<NutritionDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MealConfiguration());
        modelBuilder.ApplyConfiguration(new MealPlanConfiguration());
        modelBuilder.ApplyConfiguration(new MealPlanItemConfiguration());
    }
}
