using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.Meals.Commands.CreateMeal;

public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, Result<int>>
{
    private readonly IMealRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateMealCommandHandler(IMealRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result<int>> Handle(CreateMealCommand command, CancellationToken ct)
    {
        var meal = new Meal(
            command.Name,
            command.NutritionFacts,
            command.Ingredients,
            command.Instructions,
            command.Calories,
            command.MealType,
            command.Tags);

        _repo.Add(meal);
        await _uow.SaveChangesAsync(ct);

        return Result.Success(meal.Id);
    }
}
