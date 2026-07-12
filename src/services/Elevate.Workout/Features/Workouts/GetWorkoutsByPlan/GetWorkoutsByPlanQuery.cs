using MediatR;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.GetWorkoutsByPlan
{
    public record GetWorkoutsByPlanQuery(Guid PlanId) :
        IRequest<Result<List<WorkoutByPlanResponse>>>;
}
