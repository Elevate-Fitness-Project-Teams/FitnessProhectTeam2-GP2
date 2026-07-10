using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Enums;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.CreateMealPlanFromTarget;

public record CreateMealPlanFromTargetCommand(
    int UserId,
    MealPlanGoal Goal
) : IRequest<Result<int>>;
