using Microsoft.EntityFrameworkCore;
using Elevate.Nutrition.Domain.Entities;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Infrastructure.Persistence.Repositories;

public class MealPlanRepository : IMealPlanRepository
{
    private readonly NutritionDbContext _db;

    public MealPlanRepository(NutritionDbContext db) => _db = db;

    public async Task<MealPlan?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.MealPlans
             .Include(mp => mp.Items)
             .ThenInclude(i => i.Meal)
             .FirstOrDefaultAsync(mp => mp.Id == id, ct);

    public async Task<IEnumerable<MealPlan>> GetAllAsync(CancellationToken ct = default)
        => await _db.MealPlans
             .Include(mp => mp.Items)
             .ThenInclude(i => i.Meal)
             .AsNoTracking()
             .ToListAsync(ct);

    public void Add(MealPlan mealPlan) => _db.MealPlans.Add(mealPlan);
    public void Update(MealPlan mealPlan) => _db.MealPlans.Update(mealPlan);
    public void Delete(MealPlan mealPlan) => _db.MealPlans.Remove(mealPlan);
}
