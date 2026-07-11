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

    public IQueryable<Meal> GetAllQueryable()
        => _db.Meals.AsNoTracking();

    public IQueryable<Meal> SearchByTags(string tags)
        => _db.Meals.AsNoTracking()
             .Where(m => EF.Property<string>(m, "_tagsCsv").Contains(tags));

    public void Add(Meal meal) => _db.Meals.Add(meal);
    public void Update(Meal meal) => _db.Meals.Update(meal);
    public void Delete(Meal meal) => _db.Meals.Remove(meal);
}
