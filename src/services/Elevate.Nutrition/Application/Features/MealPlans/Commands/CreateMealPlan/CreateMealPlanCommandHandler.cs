using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.CreateMealPlan;

public class CreateMealPlanCommandHandler : IRequestHandler<CreateMealPlanCommand, Result<int>>
{
    private readonly IMealPlanRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateMealPlanCommandHandler(IMealPlanRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result<int>> Handle(CreateMealPlanCommand command, CancellationToken ct)
    {
        var mealPlan = new MealPlan(
            command.TargetCalorieRangeMin,
            command.TargetCalorieRangeMax,
            command.Goal);

        _repo.Add(mealPlan);
        await _uow.SaveChangesAsync(ct);

        return Result.Success(mealPlan.Id);
    }
}
