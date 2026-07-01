using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.Meals.Queries.SearchMealsByTags;

public class SearchMealsByTagsQueryHandler : IRequestHandler<SearchMealsByTagsQuery, Result<IEnumerable<Meal>>>
{
    private readonly IMealRepository _repo;

    public SearchMealsByTagsQueryHandler(IMealRepository repo) => _repo = repo;

    public async Task<Result<IEnumerable<Meal>>> Handle(SearchMealsByTagsQuery query, CancellationToken ct)
    {
        var meals = await _repo.SearchByTagsAsync(query.Tags, ct);
        return Result.Success(meals);
    }
}
