using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.RemoveMealPlanItem;

public class RemoveMealPlanItemCommandHandler : IRequestHandler<RemoveMealPlanItemCommand, Result>
{
    private readonly IMealPlanRepository _repo;
    private readonly IUnitOfWork _uow;

    public RemoveMealPlanItemCommandHandler(IMealPlanRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result> Handle(RemoveMealPlanItemCommand command, CancellationToken ct)
    {
        var mealPlan = await _repo.GetByIdAsync(command.MealPlanId, ct);
        if (mealPlan is null)
            return Result.Failure("MealPlan not found");

        var item = mealPlan.Items.FirstOrDefault(i => i.Id == command.MealPlanItemId);
        if (item is null)
            return Result.Failure("MealPlanItem not found");

        mealPlan.RemoveItem(item);
        _repo.Update(mealPlan);
        await _uow.SaveChangesAsync(ct);

        return Result.Success();
    }
}
