using Microsoft.EntityFrameworkCore;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Infrastructure.Persistence.Repositories;

public class MealRepository : IMealRepository
{
    private readonly NutritionDbContext _db;

    public MealRepository(NutritionDbContext db) => _db = db;

    public async Task<Meal?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.Meals.FindAsync(new object[] { id }, ct);

    public async Task<PagedResult<Meal>> GetPagedAsync(int page, int pageSize, int? minProtein = null, CancellationToken ct = default)
    {
        var query = _db.Meals.AsNoTracking();
        if (minProtein.HasValue)
            query = query.Where(m => m.ProteinGrams >= minProtein.Value);
        var total = await query.CountAsync(ct);
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);

        return new PagedResult<Meal>(items, total, page, pageSize);
    }

    public IQueryable<Meal> SearchByTags(string tags, int? minProtein = null)
    {
        var query = _db.Meals.AsNoTracking()
            .Where(m => EF.Property<string>(m, "_tagsCsv").Contains(tags));

        if (minProtein.HasValue)
            query = query.Where(m => m.ProteinGrams >= minProtein.Value);

        return query;
    }

    public void Add(Meal meal) => _db.Meals.Add(meal);
    public void Update(Meal meal) => _db.Meals.Update(meal);
    public void Delete(Meal meal) => _db.Meals.Remove(meal);
}
