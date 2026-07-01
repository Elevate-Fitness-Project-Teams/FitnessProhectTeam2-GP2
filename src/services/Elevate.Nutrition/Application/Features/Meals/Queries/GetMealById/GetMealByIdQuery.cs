using MediatR;
using Elevate.Nutrition.Domain.Common;
using Elevate.Nutrition.Domain.Entities;

namespace Elevate.Nutrition.Application.Features.Meals.Queries.GetMealById;

public record GetMealByIdQuery(int Id) : IRequest<Result<Meal?>>;
