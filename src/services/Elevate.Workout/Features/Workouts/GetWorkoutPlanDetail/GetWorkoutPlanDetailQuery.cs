using MediatR;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.GetWorkoutPlanDetail
{
    public sealed record GetWorkoutPlanDetailQuery(int PlanId) :
        IRequest<Result<WorkoutPlanDetailResponse>>;

}
