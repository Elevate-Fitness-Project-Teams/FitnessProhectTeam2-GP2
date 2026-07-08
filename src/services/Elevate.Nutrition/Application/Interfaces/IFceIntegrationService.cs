using Elevate.Nutrition.Application.Features.MealPlans.Dtos;

namespace Elevate.Nutrition.Application.Interfaces;

public interface IFceIntegrationService
{
    Task<CalorieTargetDto?> GetCalorieTargetAsync(int userId, CancellationToken ct = default);
}
