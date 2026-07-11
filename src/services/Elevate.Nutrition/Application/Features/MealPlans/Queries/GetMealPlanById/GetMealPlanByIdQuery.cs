using MediatR;
using Elevate.Nutrition.Application.Features.MealPlans.Dtos;
using Elevate.Nutrition.Domain.Common;

namespace Elevate.Nutrition.Application.Features.MealPlans.Queries.GetMealPlanById;

public record GetMealPlanByIdQuery(int Id) : IRequest<Result<MealPlanDto?>>;
