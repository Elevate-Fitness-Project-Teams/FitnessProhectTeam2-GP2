using MediatR;
using SharedKernel;

namespace Elevate.Workout.Features.Exercises.GetExcersiceDetail
{
    public record GetExerciseDetailQuery(int Id) : IRequest<Result<ExerciseDetailResponse>>;
}
