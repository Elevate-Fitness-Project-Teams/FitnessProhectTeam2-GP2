using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;

namespace Elevate.Nutrition.Application.Features.Meals.Queries.GetAllMeals;

public record GetAllMealsQuery : IRequest<Result<IEnumerable<Meal>>>;
