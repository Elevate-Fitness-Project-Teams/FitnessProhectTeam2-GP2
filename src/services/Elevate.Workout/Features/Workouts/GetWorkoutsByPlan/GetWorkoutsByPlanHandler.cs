using Elevate.Workout.Infrastructure.Presistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.GetWorkoutsByPlan
{
    public class GetWorkoutsByPlanHandler : IRequestHandler<GetWorkoutsByPlanQuery, Result<List<WorkoutByPlanResponse>>>
    {
        private readonly WorkOutDbContext _context;

        public GetWorkoutsByPlanHandler(WorkOutDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<WorkoutByPlanResponse>>> Handle(GetWorkoutsByPlanQuery request, CancellationToken cancellationToken)
        {
            //check if any plan exists in db
            bool planExists = await _context.WorkoutPlans
                .AnyAsync(p => p.Id == request.PlanId, cancellationToken);

            if (!planExists)
            {
                return Result.Failure<List<WorkoutByPlanResponse>>(new Error(
                    Code: "RES_PLAN_NOT_FOUND",
                    Message: $"Workout Plan with ID {request.PlanId} was not found.",
                    Type: ErrorType.NotFound
                ));
            }

            var workouts = await _context.Workouts
                .AsNoTracking()
                .Where(w => w.WorkoutPlanId == request.PlanId)
                .OrderBy(w => w.OrderIndex) 
                .Select(w => new WorkoutByPlanResponse(
                    w.WorkoutId,
                    w.Name,
                    w.Category.ToString(),
                    w.Difficulty.ToString(),
                    w.EstimatedDurationInMinutes,
                    w.OrderIndex
                ))
                .ToListAsync(cancellationToken);

            return Result.Success(workouts);
        }
    }
}