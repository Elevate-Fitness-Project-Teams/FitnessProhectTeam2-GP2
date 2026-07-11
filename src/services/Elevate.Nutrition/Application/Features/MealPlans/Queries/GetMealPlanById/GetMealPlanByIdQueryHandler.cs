using MediatR;
using Elevate.Nutrition.Application.Features.MealPlans.Dtos;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.MealPlans.Queries.GetMealPlanById;

public class GetMealPlanByIdQueryHandler : IRequestHandler<GetMealPlanByIdQuery, Result<MealPlanDto?>>
{
    private readonly IMealPlanRepository _repo;

    public GetMealPlanByIdQueryHandler(IMealPlanRepository repo) => _repo = repo;

    public async Task<Result<MealPlanDto?>> Handle(GetMealPlanByIdQuery query, CancellationToken ct)
    {
        var mealPlan = await _repo.GetByIdAsync(query.Id, ct);

        if (mealPlan is null)
            return Result.Failure<MealPlanDto?>("MealPlan not found", ErrorType.NotFound);

        return Result.Success<MealPlanDto?>(MealPlanDto.FromEntity(mealPlan));
    }
}
