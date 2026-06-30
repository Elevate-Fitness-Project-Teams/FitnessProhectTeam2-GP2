using Elevate.Nutrition.Domain.Entities;

namespace Elevate.Nutrition.Domain.Interfaces;

public interface IMealRepository
{
    Task<Meal?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Meal>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Meal>> SearchByTagsAsync(string tags, CancellationToken ct = default);
    void Add(Meal meal);
    void Update(Meal meal);
    void Delete(Meal meal);
}
