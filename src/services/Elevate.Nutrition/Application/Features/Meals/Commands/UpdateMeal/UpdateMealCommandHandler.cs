using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.Meals.Commands.UpdateMeal;

public class UpdateMealCommandHandler : IRequestHandler<UpdateMealCommand, Result>
{
    private readonly IMealRepository _repo;
    private readonly IUnitOfWork _uow;

    public UpdateMealCommandHandler(IMealRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result> Handle(UpdateMealCommand command, CancellationToken ct)
    {
        var meal = await _repo.GetByIdAsync(command.Id, ct);

        if (meal is null)
            return Result.Failure("Meal not found", ErrorType.NotFound);

        meal.Update(
            command.Name,
            command.NutritionFacts,
            command.Ingredients,
            command.Instructions,
            command.Calories,
            command.MealType,
            command.Tags);

        _repo.Update(meal);
        await _uow.SaveChangesAsync(ct);

        return Result.Success();
    }
}
