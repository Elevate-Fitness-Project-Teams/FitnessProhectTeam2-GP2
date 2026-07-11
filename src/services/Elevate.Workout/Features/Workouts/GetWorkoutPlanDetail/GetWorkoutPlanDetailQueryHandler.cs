using Elevate.Workout.Infrastructure.Presistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.GetWorkoutPlanDetail
{
    public sealed class GetWorkoutPlanDetailQueryHandler
     : IRequestHandler<GetWorkoutPlanDetailQuery, Result<WorkoutPlanDetailResponse>>
    {
        private readonly WorkOutDbContext _db;

        public GetWorkoutPlanDetailQueryHandler(WorkOutDbContext db) => _db = db;

        public async Task<Result<WorkoutPlanDetailResponse>> Handle(
            GetWorkoutPlanDetailQuery request, CancellationToken ct)
        {
            var plan = await _db.WorkoutPlans
                .AsNoTracking()
                .Where(p => p.Id == request.PlanId)
                .Select(p => new WorkoutPlanDetailResponse(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Difficulty.ToString(),
                    p.DurationWeeks))
                .FirstOrDefaultAsync(ct);

            if (plan is null)
                return Result.Failure<WorkoutPlanDetailResponse>(
                    new Error("RES_PLAN_NOT_FOUND", "WorkoutPlan not found", ErrorType.NotFound));
            return Result.Success(plan);
        }
    }
}
