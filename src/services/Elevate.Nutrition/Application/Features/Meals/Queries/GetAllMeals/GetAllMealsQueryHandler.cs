using MediatR;
using Elevate.Nutrition.Application.Features.Meals.Dtos;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.Meals.Queries.GetAllMeals;

public class GetAllMealsQueryHandler : IRequestHandler<GetAllMealsQuery, Result<PagedResult<MealDto>>>
{
    private readonly IMealRepository _repo;

    public GetAllMealsQueryHandler(IMealRepository repo) => _repo = repo;

    public async Task<Result<PagedResult<MealDto>>> Handle(GetAllMealsQuery query, CancellationToken ct)
    {
        var paged = await _repo.GetPagedAsync(query.Page, query.PageSize, ct);

        var dtos = paged.Items.Select(MealDto.FromEntity).ToList();

        return Result.Success(new PagedResult<MealDto>(dtos, paged.TotalCount, paged.Page, paged.PageSize));
    }
}
