using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;

namespace Elevate.Nutrition.Application.Features.MealPlans.Queries.GetAllMealPlans;

public record GetAllMealPlansQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PagedResult<MealPlan>>>;
