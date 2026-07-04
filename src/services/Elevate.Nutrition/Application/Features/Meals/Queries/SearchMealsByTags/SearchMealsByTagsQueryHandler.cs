using MediatR;
using Microsoft.EntityFrameworkCore;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.Meals.Queries.SearchMealsByTags;

public class SearchMealsByTagsQueryHandler : IRequestHandler<SearchMealsByTagsQuery, Result<PagedResult<Meal>>>
{
    private readonly IMealRepository _repo;

    public SearchMealsByTagsQueryHandler(IMealRepository repo) => _repo = repo;

    public async Task<Result<PagedResult<Meal>>> Handle(SearchMealsByTagsQuery query, CancellationToken ct)
    {
        var q = _repo.SearchByTags(query.Tags);

        var total = await q.CountAsync(ct);
        var items = await q.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToListAsync(ct);

        return Result.Success(new PagedResult<Meal>(items, total, query.Page, query.PageSize));
    }
}
