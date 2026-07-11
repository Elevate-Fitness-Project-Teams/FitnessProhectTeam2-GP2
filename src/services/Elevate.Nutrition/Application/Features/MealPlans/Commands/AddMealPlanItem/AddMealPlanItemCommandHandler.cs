using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.AddMealPlanItem;

public class AddMealPlanItemCommandHandler : IRequestHandler<AddMealPlanItemCommand, Result>
{
    private readonly IMealPlanRepository _mealPlanRepo;
    private readonly IMealRepository _mealRepo;
    private readonly IUnitOfWork _uow;

    public AddMealPlanItemCommandHandler(
        IMealPlanRepository mealPlanRepo,
        IMealRepository mealRepo,
        IUnitOfWork uow)
    {
        _mealPlanRepo = mealPlanRepo;
        _mealRepo = mealRepo;
        _uow = uow;
    }

    public async Task<Result> Handle(AddMealPlanItemCommand command, CancellationToken ct)
    {
        var mealPlan = await _mealPlanRepo.GetByIdAsync(command.MealPlanId, ct);
        if (mealPlan is null)
            return Result.Failure("MealPlan not found", ErrorType.NotFound);

        var meal = await _mealRepo.GetByIdAsync(command.MealId, ct);
        if (meal is null)
            return Result.Failure("Meal not found", ErrorType.NotFound);

        var item = new MealPlanItem(command.MealId, command.ServingCount);
        mealPlan.AddItem(item);

        _mealPlanRepo.Update(mealPlan);
        await _uow.SaveChangesAsync(ct);

        return Result.Success();
    }
}
