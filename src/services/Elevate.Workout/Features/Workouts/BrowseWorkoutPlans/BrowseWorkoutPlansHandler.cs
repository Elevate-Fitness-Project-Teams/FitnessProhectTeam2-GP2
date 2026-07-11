using Elevate.Workout.Infrastructure.Presistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Pagniation;

namespace Elevate.Workout.Features.Workouts.BrowseWorkoutPlans
{
    public class BrowseWorkoutPlansHandler : IRequestHandler<BrowseWorkoutPlansQuery, Result<PagedList<WorkoutPlanResponse>>>
    {
        private readonly WorkOutDbContext _context;

        public BrowseWorkoutPlansHandler(WorkOutDbContext context) => _context = context;

        public async Task<Result<PagedList<WorkoutPlanResponse>>> Handle(BrowseWorkoutPlansQuery request, CancellationToken cancellationToken)
        {
            var query = _context.WorkoutPlans
                .AsNoTracking()
                .Select(wp => new WorkoutPlanResponse(wp.Id, wp.Name, wp.Description, wp.Difficulty.ToString()));

            var pagedList = await PagedList<WorkoutPlanResponse>.CreateAsync(query, request.PageNumber, request.PageSize, cancellationToken);

            return Result.Success(pagedList);
        }
    }
}
