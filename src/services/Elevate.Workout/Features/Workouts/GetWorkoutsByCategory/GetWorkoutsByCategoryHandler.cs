using Elevate.Workout.Domain.Enums;
using Elevate.Workout.Infrastructure.Presistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Elevate.Workout.Features.Workouts.GetWorkoutsByCategory
{
    public class GetWorkoutsByCategoryHandler : IRequestHandler<GetWorkoutsByCategoryQuery,
        Result<List<WorkoutByCategoryResponse>>>
    {
        private readonly WorkOutDbContext _context;

        public GetWorkoutsByCategoryHandler(WorkOutDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<WorkoutByCategoryResponse>>> Handle(GetWorkoutsByCategoryQuery request, CancellationToken cancellationToken)
        {
            if (!Enum.TryParse<WorkoutCategory>(request.CategoryName, true, out var parsedCategory))
            {
                return Result.Failure<List<WorkoutByCategoryResponse>>(new Error(
                    Code: "RES_NOT_FOUND",
                    Message: $"Category '{request.CategoryName}' is not a recognized workout category.",
                    Type: ErrorType.NotFound
                ));
            }

            var workouts = await _context.Workouts
                .AsNoTracking()
                .Where(w => w.Category == parsedCategory)
                .Select(w => new WorkoutByCategoryResponse(
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