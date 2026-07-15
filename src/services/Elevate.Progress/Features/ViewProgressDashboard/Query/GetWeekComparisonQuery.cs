using Elevate.Progress.Features.ViewProgressDashboard.DTOS;
using Elevate.Progress.Infrastructure.Persistence.ProgressDbContext;
using Elevate.Progress.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

public record GetWeekComparisonQuery(
    GetWeekComparisonRequestDto RequestDto)
    : IRequest<GetWeekComparisonResponseDto>;

public class GetWeekComparisonQueryHandler
    : IRequestHandler<GetWeekComparisonQuery, GetWeekComparisonResponseDto>
{
    private readonly ProgressDbContext _context;

    public GetWeekComparisonQueryHandler(ProgressDbContext context)
    {
        _context = context;
    }

    public async Task<GetWeekComparisonResponseDto> Handle(
        GetWeekComparisonQuery request,
        CancellationToken cancellationToken)
    {
        if (request.RequestDto.UserId == Guid.Empty)
            throw new InvalidUserIdException();

        var today = DateTime.UtcNow.Date;

        var currentWeekStart = today.AddDays(-6);
        var previousWeekStart = currentWeekStart.AddDays(-7);
        var previousWeekEnd = currentWeekStart.AddDays(-1);

        var currentWeekWorkouts = await _context.WorkoutLogs
            .AsNoTracking()
            .Where(x =>
                x.UserId == request.RequestDto.UserId &&
                x.CompletedAt.Date >= currentWeekStart &&
                x.CompletedAt.Date <= today)
            .ToListAsync(cancellationToken);

        var previousWeekWorkouts = await _context.WorkoutLogs
            .AsNoTracking()
            .Where(x =>
                x.UserId == request.RequestDto.UserId &&
                x.CompletedAt.Date >= previousWeekStart &&
                x.CompletedAt.Date <= previousWeekEnd)
            .ToListAsync(cancellationToken);

        return new GetWeekComparisonResponseDto
        {
            WorkoutDifference =
                currentWeekWorkouts.Count - previousWeekWorkouts.Count,

            CaloriesDifference =
                currentWeekWorkouts.Sum(x => x.CaloriesBurned) -
                previousWeekWorkouts.Sum(x => x.CaloriesBurned),

            WeightDifference = 0
        };
    }
}