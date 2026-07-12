using MediatR;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.GetWorkoutPlanDetail
{
    public sealed record GetWorkoutPlanDetailQuery(Guid PlanId) :
        IRequest<Result<WorkoutPlanDetailResponse>>;

}
