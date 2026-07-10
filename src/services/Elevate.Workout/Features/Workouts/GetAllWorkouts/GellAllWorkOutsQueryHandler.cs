using Elevate.Workout.Domain.Enums;
using Elevate.Workout.Infrastructure.Presistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Pagniation;

namespace Elevate.Workout.Features.Workouts.GetAllWorkouts
{
    public class GetAllWorkoutsQueryHandler : IRequestHandler<GetAllWorkoutsQuery, Result<PagedList<WorkoutResponse>>>
    {
        private readonly WorkOutDbContext _context;

        public GetAllWorkoutsQueryHandler(WorkOutDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PagedList<WorkoutResponse>>> Handle(GetAllWorkoutsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Workouts
                .Include(w => w.WorkoutPlan)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Category))
            {
                var categoryEnum = request.Category.ToLower() == "full-body"
                    ? Domain.Enums.WorkoutCategory.FullBody
                    : (Domain.Enums.WorkoutCategory)Enum.Parse(typeof(Domain.Enums.WorkoutCategory), request.Category, true);

                query = query.Where(w => w.Category == categoryEnum);
            }

            if (!string.IsNullOrWhiteSpace(request.Difficulty))
            {
                var difficultyEnum = (DifficultyLevel)Enum.Parse(typeof(DifficultyLevel), request.Difficulty, true);
                query = query.Where(w => w.Difficulty == difficultyEnum);
            }

            if (request.Duration.HasValue)
            {
                query = query.Where(w => w.EstimatedDurationInMinutes == request.Duration.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(w => w.Name.Contains(request.Search));
            }

            query = query.OrderBy(w => w.OrderIndex);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(w => new WorkoutResponse(
                    w.WorkoutId,
                    w.Name,
                    w.Category,
                    w.Difficulty,
                    w.EstimatedDurationInMinutes,
                    w.OrderIndex,
                    w.WorkoutPlan != null ? w.WorkoutPlan.Name : null
                ))
                .ToListAsync(cancellationToken);

            var pagedList = new PagedList<WorkoutResponse>(items, request.Page, request.PageSize, totalCount);

            return Result.Success(pagedList);
        }
    }
}
