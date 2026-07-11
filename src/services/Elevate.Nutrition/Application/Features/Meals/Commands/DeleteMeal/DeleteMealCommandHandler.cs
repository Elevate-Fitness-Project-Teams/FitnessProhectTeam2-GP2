using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.Meals.Commands.DeleteMeal;

public class DeleteMealCommandHandler : IRequestHandler<DeleteMealCommand, Result>
{
    private readonly IMealRepository _repo;
    private readonly IUnitOfWork _uow;

    public DeleteMealCommandHandler(IMealRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result> Handle(DeleteMealCommand command, CancellationToken ct)
    {
        var meal = await _repo.GetByIdAsync(command.Id, ct);

        if (meal is null)
            return Result.Failure("Meal not found", ErrorType.NotFound);

        _repo.Delete(meal);
        await _uow.SaveChangesAsync(ct);

        return Result.Success();
    }
}
