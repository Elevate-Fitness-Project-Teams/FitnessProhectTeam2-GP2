using Microsoft.EntityFrameworkCore;
using Elevate.Nutrition.Domain.Common;
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

    public async Task<PagedResult<MealPlan>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var query = _db.MealPlans
            .Include(mp => mp.Items)
            .ThenInclude(i => i.Meal)
            .AsNoTracking();

        var total = await query.CountAsync(ct);
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);

        return new PagedResult<MealPlan>(items, total, page, pageSize);
    }

    public void Add(MealPlan mealPlan) => _db.MealPlans.Add(mealPlan);
    public void Update(MealPlan mealPlan) => _db.MealPlans.Update(mealPlan);
    public void Delete(MealPlan mealPlan) => _db.MealPlans.Remove(mealPlan);
}
