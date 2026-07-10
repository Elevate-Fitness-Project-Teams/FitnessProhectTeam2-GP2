using MediatR;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.GetWorkoutsByCategory
{
    public record GetWorkoutsByCategoryQuery(string CategoryName) 
        : IRequest<Result<List<WorkoutByCategoryResponse>>>;
}
