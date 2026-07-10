using Elevate.Workout.Infrastructure.Presistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Elevate.Workout.Features.Exercises.GetExcersiceDetail
{
    public class GetExerciseDetailHandler : IRequestHandler<GetExerciseDetailQuery, Result<ExerciseDetailResponse>>
    {
        private readonly WorkOutDbContext _context;

        public GetExerciseDetailHandler(WorkOutDbContext context)
        {
            _context = context;
        }

        public async Task<Result<ExerciseDetailResponse>> Handle(GetExerciseDetailQuery request, CancellationToken cancellationToken)
        {
            var exerciseDetail = await _context.Exercises
                .Where(e => e.Id == request.Id)
                .Select(e => new ExerciseDetailResponse(
                    e.Id,
                    e.Name,
                    e.TargetMuscles,
                    e.Equipment,
                    e.Description,
                    e.VideoUrl
                ))
                .FirstOrDefaultAsync(cancellationToken);

            if (exerciseDetail == null)
            {
                return Result.Failure<ExerciseDetailResponse>(new Error(
                    Code: "RES_EXERCISE_NOT_FOUND",
                    Message: $"Exercise with ID {request.Id} was not found.",
                    Type: ErrorType.NotFound
                ));
            }

            return Result.Success(exerciseDetail);
        }
    }
}
