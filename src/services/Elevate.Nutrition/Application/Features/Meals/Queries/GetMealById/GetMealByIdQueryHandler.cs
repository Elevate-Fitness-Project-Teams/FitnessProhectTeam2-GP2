using MediatR;
using Elevate.Nutrition.Application.Features.Meals.Dtos;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.Meals.Queries.GetMealById;

public class GetMealByIdQueryHandler : IRequestHandler<GetMealByIdQuery, Result<MealDto?>>
{
    private readonly IMealRepository _repo;

    public GetMealByIdQueryHandler(IMealRepository repo) => _repo = repo;

    public async Task<Result<MealDto?>> Handle(GetMealByIdQuery query, CancellationToken ct)
    {
        var meal = await _repo.GetByIdAsync(query.Id, ct);

        if (meal is null)
            return Result.Failure<MealDto?>("Meal not found", ErrorType.NotFound);

        return Result.Success<MealDto?>(MealDto.FromEntity(meal));
    }
}
