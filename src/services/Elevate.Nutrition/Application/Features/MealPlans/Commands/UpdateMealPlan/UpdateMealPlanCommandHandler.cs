using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.UpdateMealPlan;

public class UpdateMealPlanCommandHandler : IRequestHandler<UpdateMealPlanCommand, Result>
{
    private readonly IMealPlanRepository _repo;
    private readonly IUnitOfWork _uow;

    public UpdateMealPlanCommandHandler(IMealPlanRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result> Handle(UpdateMealPlanCommand command, CancellationToken ct)
    {
        var mealPlan = await _repo.GetByIdAsync(command.Id, ct);

        if (mealPlan is null)
            return Result.Failure("MealPlan not found", ErrorType.NotFound);

        mealPlan.UpdateCalorieRange(command.TargetCalorieRangeMin, command.TargetCalorieRangeMax);
        mealPlan.UpdateGoal(command.Goal);

        _repo.Update(mealPlan);
        await _uow.SaveChangesAsync(ct);

        return Result.Success();
    }
}
