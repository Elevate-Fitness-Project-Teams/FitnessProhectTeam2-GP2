using MediatR;
using Elevate.Nutrition.Application.Features.Meals.Dtos;
using Elevate.Nutrition.Domain.Common;

namespace Elevate.Nutrition.Application.Features.Meals.Queries.SearchMealsByTags;

public record SearchMealsByTagsQuery(string Tags, int Page = 1, int PageSize = 10) : IRequest<Result<PagedResult<MealDto>>>;
