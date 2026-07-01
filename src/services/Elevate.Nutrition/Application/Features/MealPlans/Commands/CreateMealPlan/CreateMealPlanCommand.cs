using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Enums;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.CreateMealPlan;

public record CreateMealPlanCommand(
    int TargetCalorieRangeMin,
    int TargetCalorieRangeMax,
    MealPlanGoal Goal
) : IRequest<Result<int>>;
