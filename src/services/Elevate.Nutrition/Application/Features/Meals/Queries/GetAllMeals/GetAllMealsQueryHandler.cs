using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.Meals.Queries.GetAllMeals;

public class GetAllMealsQueryHandler : IRequestHandler<GetAllMealsQuery, Result<IEnumerable<Meal>>>
{
    private readonly IMealRepository _repo;

    public GetAllMealsQueryHandler(IMealRepository repo) => _repo = repo;

    public async Task<Result<IEnumerable<Meal>>> Handle(GetAllMealsQuery query, CancellationToken ct)
    {
        var meals = await _repo.GetAllAsync(ct);
        return Result.Success(meals);
    }
}
