using Elevate.Workout.Infrastructure.Presistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.GetWorkoutDetails
{
    public class GetWorkoutDetailsHandler : IRequestHandler<GetWorkoutDetailsQuery, Result<WorkoutDetailsResponse>>
    {
        private readonly WorkOutDbContext _context;

        public GetWorkoutDetailsHandler(WorkOutDbContext context)
        {
            _context = context;
        }

        public async Task<Result<WorkoutDetailsResponse>> Handle(GetWorkoutDetailsQuery request, CancellationToken cancellationToken)
        {
            var workoutResponse = await _context.Workouts
             .AsNoTracking()
             .Where(w => w.WorkoutId == request.Id)
             .Select(w => new WorkoutDetailsResponse(
             w.WorkoutId,
             w.Name,
             w.Category.ToString(),
             w.Difficulty.ToString(),
             w.EstimatedDurationInMinutes,
             w.Exercises
             .OrderBy(we => we.OrderIndex)
             .Select(we => new WorkoutExerciseResponse(
                 we.Id,
                 we.Exercise.Name,
                 we.OrderIndex
             ))
             .ToList()
     ))
     .FirstOrDefaultAsync(cancellationToken);
            if (workoutResponse == null)
            {
                return Result.Failure<WorkoutDetailsResponse>(new Error(
                    Code: "RES_WORKOUT_NOT_FOUND",
                    Message: $"Workout with ID {request.Id} was not found.",
                    Type: ErrorType.NotFound
                ));
            }

            return Result.Success(workoutResponse);
        }
    }
}