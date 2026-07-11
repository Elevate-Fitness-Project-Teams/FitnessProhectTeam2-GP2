using Elevate.Nutrition.Application.Features.MealPlans.Dtos;
using Elevate.Nutrition.Application.Interfaces;

namespace Elevate.Nutrition.Infrastructure.Services;

public class NullFceIntegrationService : IFceIntegrationService
{
    public Task<CalorieTargetDto?> GetCalorieTargetAsync(int userId, CancellationToken ct = default)
        => Task.FromResult<CalorieTargetDto?>(null);
}
