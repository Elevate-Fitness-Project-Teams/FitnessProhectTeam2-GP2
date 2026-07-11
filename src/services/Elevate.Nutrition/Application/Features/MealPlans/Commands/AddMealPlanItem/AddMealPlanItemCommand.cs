using MediatR;
using Elevate.Nutrition.Domain.Common;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.AddMealPlanItem;

public record AddMealPlanItemCommand(
    int MealPlanId,
    int MealId,
    int ServingCount
) : IRequest<Result>;
