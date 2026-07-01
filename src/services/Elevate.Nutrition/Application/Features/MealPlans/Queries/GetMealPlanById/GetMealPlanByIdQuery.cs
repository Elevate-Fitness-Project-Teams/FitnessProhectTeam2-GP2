using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;

namespace Elevate.Nutrition.Application.Features.MealPlans.Queries.GetMealPlanById;

public record GetMealPlanByIdQuery(int Id) : IRequest<Result<MealPlan?>>;
