using MediatR;
using Elevate.Nutrition.Domain.Common;

namespace Elevate.Nutrition.Application.Features.Meals.Commands.DeleteMeal;

public record DeleteMealCommand(int Id) : IRequest<Result>;
