using Elevate.Workout.Features.Workouts.GetWorkoutsByPlan;
using MediatR;
using SharedKernel;
using SharedKernel.Pagniation;

namespace Elevate.Workout.Features.Workouts.BrowseWorkoutPlans
{
    public record BrowseWorkoutPlansQuery(int PageNumber, int PageSize) 
        : IRequest<Result<PagedList<WorkoutPlanResponse>>>;
}
