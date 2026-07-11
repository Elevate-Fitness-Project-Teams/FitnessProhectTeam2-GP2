using MediatR;
using Elevate.Nutrition.Application.Features.Meals.Dtos;
using Elevate.Nutrition.Domain.Common;

namespace Elevate.Nutrition.Application.Features.Meals.Queries.GetMealById;

public record GetMealByIdQuery(int Id) : IRequest<Result<MealDto?>>;
