using Elevate.Progress.Domain.Enums;
using Elevate.Progress.Features.ViewProgressDashboard.DTOS;
using Elevate.Progress.Features.ViewProgressDashboard.Exception;
using Elevate.Progress.Infrastructure.Persistence;
using Elevate.Progress.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.ViewProgressDashboard.Query
{
    public record GetWorkoutHistoryQuery(
        GetWorkoutHistoryRequestDto RequestDto)
        : IRequest<GetWorkoutHistoryResponseDto>;

    public class GetWorkoutHistoryQueryHandler
        : IRequestHandler<GetWorkoutHistoryQuery, GetWorkoutHistoryResponseDto>
    {
        private readonly ProgressDbContext _context;

        public GetWorkoutHistoryQueryHandler(ProgressDbContext context)
        {
            _context = context;
        }

        public async Task<GetWorkoutHistoryResponseDto> Handle(
            GetWorkoutHistoryQuery request,
            CancellationToken cancellationToken)
        {
            if (request.RequestDto.UserId == Guid.Empty)
                throw new InvalidUserIdException();

            if (request.RequestDto.StartDate.HasValue &&
                request.RequestDto.EndDate.HasValue &&
                request.RequestDto.StartDate > request.RequestDto.EndDate)
            {
                throw new InvalidDateException();
            }

            var query = _context.WorkoutLogs
                .AsNoTracking()
                .Where(x => x.UserId == request.RequestDto.UserId);

            if (request.RequestDto.Period.HasValue)
            {
                var today = DateTime.UtcNow.Date;

                switch (request.RequestDto.Period.Value)
                {
                    case ProgressPeriod.Week:
                        query = query.Where(x => x.CompletedAt >= today.AddDays(-7));
                        break;

                    case ProgressPeriod.Month:
                        query = query.Where(x => x.CompletedAt >= today.AddMonths(-1));
                        break;

                    case ProgressPeriod.Year:
                        query = query.Where(x => x.CompletedAt >= today.AddYears(-1));
                        break;

                    case ProgressPeriod.All:
                        break;
                }
            }

            if (request.RequestDto.StartDate.HasValue)
            {
                query = query.Where(x => x.CompletedAt >= request.RequestDto.StartDate.Value);
            }

            if (request.RequestDto.EndDate.HasValue)
            {
                query = query.Where(x => x.CompletedAt <= request.RequestDto.EndDate.Value);
            }

            var workoutHistory = await query
                .OrderByDescending(x => x.CompletedAt)
                .Select(x => new WorkoutHistoryRecordDto
                {
                    CompletedAt = x.CompletedAt,
                    Duration = x.Duration,
                    CaloriesBurned = x.CaloriesBurned
                })
                .ToListAsync(cancellationToken);

            return new GetWorkoutHistoryResponseDto
            {
                WorkoutHistory = workoutHistory
            };
        }
    }
}