using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;

namespace Elevate.Nutrition.Application.Features.Meals.Queries.SearchMealsByTags;

public record SearchMealsByTagsQuery(string Tags) : IRequest<Result<IEnumerable<Meal>>>;
