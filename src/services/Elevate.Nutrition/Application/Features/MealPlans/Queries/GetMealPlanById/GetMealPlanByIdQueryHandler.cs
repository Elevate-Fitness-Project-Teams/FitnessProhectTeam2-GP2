using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.MealPlans.Queries.GetMealPlanById;

public class GetMealPlanByIdQueryHandler : IRequestHandler<GetMealPlanByIdQuery, Result<MealPlan?>>
{
    private readonly IMealPlanRepository _repo;

    public GetMealPlanByIdQueryHandler(IMealPlanRepository repo) => _repo = repo;

    public async Task<Result<MealPlan?>> Handle(GetMealPlanByIdQuery query, CancellationToken ct)
    {
        var mealPlan = await _repo.GetByIdAsync(query.Id, ct);

        if (mealPlan is null)
            return Result.Failure<MealPlan?>("MealPlan not found");

        return Result.Success<MealPlan?>(mealPlan);
    }
}
