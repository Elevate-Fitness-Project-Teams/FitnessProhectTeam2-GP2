using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;

namespace Elevate.Nutrition.Application.Features.Meals.Queries.GetAllMeals;

public record GetAllMealsQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PagedResult<Meal>>>;
