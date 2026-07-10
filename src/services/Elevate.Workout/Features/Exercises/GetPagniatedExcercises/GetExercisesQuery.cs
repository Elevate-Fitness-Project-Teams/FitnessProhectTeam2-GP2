using MediatR;
using SharedKernel;
using SharedKernel.Pagniation;

namespace Elevate.Workout.Features.Exercises.GetPagniatedExcercise
{
    public record GetExercisesQuery(int PageNumber = 1, int PageSize = 10) : 
        IRequest<Result<PagedList<ExerciseResponse>>>;
}
