using MediatR;
using Elevate.Nutrition.Application.Features.MealPlans.Dtos;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.MealPlans.Queries.GetAllMealPlans;

public class GetAllMealPlansQueryHandler : IRequestHandler<GetAllMealPlansQuery, Result<PagedResult<MealPlanDto>>>
{
    private readonly IMealPlanRepository _repo;

    public GetAllMealPlansQueryHandler(IMealPlanRepository repo) => _repo = repo;

    public async Task<Result<PagedResult<MealPlanDto>>> Handle(GetAllMealPlansQuery query, CancellationToken ct)
    {
        var paged = await _repo.GetPagedAsync(query.Page, query.PageSize, ct);

        var dtos = paged.Items.Select(MealPlanDto.FromEntity).ToList();

        return Result.Success(new PagedResult<MealPlanDto>(dtos, paged.TotalCount, paged.Page, paged.PageSize));
    }
}
