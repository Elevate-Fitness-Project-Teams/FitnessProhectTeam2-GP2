using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;

namespace Elevate.Nutrition.Domain.Interfaces;

public interface IMealPlanRepository
{
    Task<MealPlan?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<PagedResult<MealPlan>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);
    void Add(MealPlan mealPlan);
    void Update(MealPlan mealPlan);
    void Delete(MealPlan mealPlan);
}
