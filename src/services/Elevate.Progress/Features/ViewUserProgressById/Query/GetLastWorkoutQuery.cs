using Elevate.Progress.Features.ViewUserProgressById.DTOS;
using Elevate.Progress.Infrastructure.Persistence.ProgressDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.ViewUserProgressById.Query
{
    public record GetLastWorkoutQuery(GetLastWorkoutRequestDto RequestDto)
        : IRequest<GetLastWorkoutResponseDto?>;

    public class GetLastWorkoutQueryHandler
        : IRequestHandler<GetLastWorkoutQuery, GetLastWorkoutResponseDto?>
    {
        private readonly ProgressDbContext _context;

        public GetLastWorkoutQueryHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<GetLastWorkoutResponseDto?> Handle(
            GetLastWorkoutQuery request,
            CancellationToken cancellationToken)
        {
            var lastWorkout = await _context.WorkoutLogs
                .AsNoTracking()
                .Where(x => x.UserId == request.RequestDto.UserId)
                .OrderByDescending(x => x.CompletedAt)
                .FirstOrDefaultAsync(cancellationToken);

            if (lastWorkout is null)
            {
                return null;
            }

            return new GetLastWorkoutResponseDto
            {
                LastWorkoutDate = lastWorkout.CompletedAt
            };
        }
    }
}