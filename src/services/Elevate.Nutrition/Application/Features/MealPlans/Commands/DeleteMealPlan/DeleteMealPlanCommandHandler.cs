using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.DeleteMealPlan;

public class DeleteMealPlanCommandHandler : IRequestHandler<DeleteMealPlanCommand, Result>
{
    private readonly IMealPlanRepository _repo;
    private readonly IUnitOfWork _uow;

    public DeleteMealPlanCommandHandler(IMealPlanRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result> Handle(DeleteMealPlanCommand command, CancellationToken ct)
    {
        var mealPlan = await _repo.GetByIdAsync(command.Id, ct);

        if (mealPlan is null)
            return Result.Failure("MealPlan not found", ErrorType.NotFound);

        _repo.Delete(mealPlan);
        await _uow.SaveChangesAsync(ct);

        return Result.Success();
    }
}
