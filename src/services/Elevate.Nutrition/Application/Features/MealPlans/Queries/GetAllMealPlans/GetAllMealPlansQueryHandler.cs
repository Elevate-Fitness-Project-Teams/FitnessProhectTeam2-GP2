using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.MealPlans.Queries.GetAllMealPlans;

public class GetAllMealPlansQueryHandler : IRequestHandler<GetAllMealPlansQuery, Result<IEnumerable<MealPlan>>>
{
    private readonly IMealPlanRepository _repo;

    public GetAllMealPlansQueryHandler(IMealPlanRepository repo) => _repo = repo;

    public async Task<Result<IEnumerable<MealPlan>>> Handle(GetAllMealPlansQuery query, CancellationToken ct)
    {
        var mealPlans = await _repo.GetAllAsync(ct);
        return Result.Success(mealPlans);
    }
}
