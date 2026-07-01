using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Enums;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.UpdateMealPlan;

public record UpdateMealPlanCommand(
    int Id,
    int TargetCalorieRangeMin,
    int TargetCalorieRangeMax,
    MealPlanGoal Goal
) : IRequest<Result>;
