using MediatR;
using Elevate.Nutrition.Domain.Common;

namespace Elevate.Nutrition.Application.Features.MealPlans.Commands.DeleteMealPlan;

public record DeleteMealPlanCommand(int Id) : IRequest<Result>;
