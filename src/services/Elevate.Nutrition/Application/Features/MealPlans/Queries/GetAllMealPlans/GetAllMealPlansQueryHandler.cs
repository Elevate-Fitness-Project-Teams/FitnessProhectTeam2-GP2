using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.MealPlans.Queries.GetAllMealPlans;

public class GetAllMealPlansQueryHandler : IRequestHandler<GetAllMealPlansQuery, Result<PagedResult<MealPlan>>>
{
    private readonly IMealPlanRepository _repo;

    public GetAllMealPlansQueryHandler(IMealPlanRepository repo) => _repo = repo;

    public async Task<Result<PagedResult<MealPlan>>> Handle(GetAllMealPlansQuery query, CancellationToken ct)
    {
        var result = await _repo.GetPagedAsync(query.Page, query.PageSize, ct);
        return Result.Success(result);
    }
}
