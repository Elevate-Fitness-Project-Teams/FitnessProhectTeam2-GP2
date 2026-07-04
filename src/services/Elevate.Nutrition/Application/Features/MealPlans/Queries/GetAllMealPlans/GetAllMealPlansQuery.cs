using MediatR;
using Elevate.Nutrition.Application.Features.MealPlans.Dtos;
using Elevate.Nutrition.Domain.Common;

namespace Elevate.Nutrition.Application.Features.MealPlans.Queries.GetAllMealPlans;

public record GetAllMealPlansQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PagedResult<MealPlanDto>>>;
