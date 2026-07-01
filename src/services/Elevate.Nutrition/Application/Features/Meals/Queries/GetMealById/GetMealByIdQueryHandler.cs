using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.Meals.Queries.GetMealById;

public class GetMealByIdQueryHandler : IRequestHandler<GetMealByIdQuery, Result<Meal?>>
{
    private readonly IMealRepository _repo;

    public GetMealByIdQueryHandler(IMealRepository repo) => _repo = repo;

    public async Task<Result<Meal?>> Handle(GetMealByIdQuery query, CancellationToken ct)
    {
        var meal = await _repo.GetByIdAsync(query.Id, ct);

        if (meal is null)
            return Result.Failure<Meal?>("Meal not found");

        return Result.Success<Meal?>(meal);
    }
}
