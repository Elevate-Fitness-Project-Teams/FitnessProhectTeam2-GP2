using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.Meals.Queries.GetAllMeals;

public class GetAllMealsQueryHandler : IRequestHandler<GetAllMealsQuery, Result<PagedResult<Meal>>>
{
    private readonly IMealRepository _repo;

    public GetAllMealsQueryHandler(IMealRepository repo) => _repo = repo;

    public async Task<Result<PagedResult<Meal>>> Handle(GetAllMealsQuery query, CancellationToken ct)
    {
        var result = await _repo.GetPagedAsync(query.Page, query.PageSize, ct);
        return Result.Success(result);
    }
}
