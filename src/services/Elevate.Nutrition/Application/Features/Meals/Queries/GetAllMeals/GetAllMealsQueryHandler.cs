using MediatR;
using Microsoft.EntityFrameworkCore;
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
        var q = _repo.GetAllQueryable();

        if (query.MinProtein.HasValue)
            q = q.Where(m => m.ProteinGrams >= query.MinProtein.Value);

        var total = await q.CountAsync(ct);
        var items = await q.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToListAsync(ct);

        var dtos = items.Select(MealDto.FromEntity).ToList();

        return Result.Success(new PagedResult<MealDto>(dtos, total, query.Page, query.PageSize));
    }
}
