using MediatR;
using Elevate.Nutrition.Application.Features.MealPlans.Dtos;
using Elevate.Nutrition.Application.Interfaces;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;
using Elevate.Nutrition.Domain.Interfaces;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.CreateMealPlanFromTarget;

public class CreateMealPlanFromTargetCommandHandler : IRequestHandler<CreateMealPlanFromTargetCommand, Result<int>>
{
    private readonly IFceIntegrationService _fce;
    private readonly IMealPlanRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateMealPlanFromTargetCommandHandler(
        IFceIntegrationService fce,
        IMealPlanRepository repo,
        IUnitOfWork uow)
    {
        _fce = fce;
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result<int>> Handle(CreateMealPlanFromTargetCommand command, CancellationToken ct)
    {
        CalorieTargetDto? target;

        try
        {
            target = await _fce.GetCalorieTargetAsync(command.UserId, ct);
        }
        catch (HttpRequestException)
        {
            return Result.Failure<int>("Calorie target service unavailable");
        }
        catch (TaskCanceledException)
        {
            return Result.Failure<int>("Calorie target service timed out");
        }

        if (target is null)
            return Result.Failure<int>($"Calorie target not found for user {command.UserId}", ErrorType.NotFound);

        var buffer = 200;
        var mealPlan = new MealPlan(
            target.TargetCalories - buffer,
            target.TargetCalories + buffer,
            command.Goal);

        _repo.Add(mealPlan);
        await _uow.SaveChangesAsync(ct);

        return Result.Success(mealPlan.Id);
    }
}
