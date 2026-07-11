using MediatR;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.GetWorkoutsByPlan
{
    public record GetWorkoutsByPlanQuery(int PlanId) :
        IRequest<Result<List<WorkoutByPlanResponse>>>;
}
