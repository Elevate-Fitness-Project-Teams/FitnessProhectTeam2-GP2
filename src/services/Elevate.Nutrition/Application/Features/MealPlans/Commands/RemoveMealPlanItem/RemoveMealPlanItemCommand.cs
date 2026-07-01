using MediatR;
using Elevate.Nutrition.Domain.Common;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.RemoveMealPlanItem;

public record RemoveMealPlanItemCommand(
    int MealPlanId,
    int MealPlanItemId
) : IRequest<Result>;
