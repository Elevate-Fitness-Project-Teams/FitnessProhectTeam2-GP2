using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;

namespace Elevate.Nutrition.Domain.Interfaces;

public interface IMealRepository
{
    Task<Meal?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<PagedResult<Meal>> GetPagedAsync(int page, int pageSize, int? minProtein = null, CancellationToken ct = default);
    IQueryable<Meal> SearchByTags(string tags, int? minProtein = null);
    void Add(Meal meal);
    void Update(Meal meal);
    void Delete(Meal meal);
}
