using Elevate.Nutrition.Domain.Interfaces;
using Elevate.Nutrition.Infrastructure.Persistence;

namespace Elevate.Nutrition.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly NutritionDbContext _db;

    public UnitOfWork(NutritionDbContext db) => _db = db;

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        => await _db.SaveChangesAsync(ct);
}
