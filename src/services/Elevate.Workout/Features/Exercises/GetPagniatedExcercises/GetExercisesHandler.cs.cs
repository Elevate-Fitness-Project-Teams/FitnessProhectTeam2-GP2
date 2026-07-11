using Elevate.Workout.Infrastructure.Presistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Pagniation;

namespace Elevate.Workout.Features.Exercises.GetPagniatedExcercise
{
    public class GetExercisesHandler : IRequestHandler<GetExercisesQuery, Result<PagedList<ExerciseResponse>>>
    {
        private readonly WorkOutDbContext _context;

        public GetExercisesHandler(WorkOutDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PagedList<ExerciseResponse>>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Workouts
                .AsNoTracking()
                .Select(w => new ExerciseResponse(
                    w.WorkoutId,
                    w.Name,
                    w.Category.ToString(),  
                    w.Difficulty.ToString() 
                ));

            var paginatedWorkouts = await PagedList<ExerciseResponse>.CreateAsync(
                query,
                request.PageNumber,
                request.PageSize);

            return Result.Success(paginatedWorkouts);
        }
    }
}
