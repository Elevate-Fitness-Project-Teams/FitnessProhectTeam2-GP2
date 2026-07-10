using Elevate.Progress.Features.LogWorkoutCompletion.DTOS;
using Elevate.Progress.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Progress.Features.LogWorkoutCompletion.Query;

public record GetWorkoutSessionQuery(
    Guid SessionId,
    Guid UserId)
    : IRequest<WorkoutSessionDto?>;

public class GetWorkoutSessionQueryHandler
    : IRequestHandler<GetWorkoutSessionQuery, WorkoutSessionDto?>
{
    private readonly ProgressDbContext _context;

    public GetWorkoutSessionQueryHandler(ProgressDbContext context)
    {
        _context = context;
    }

    public async Task<WorkoutSessionDto?> Handle(
        GetWorkoutSessionQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.WorkoutSessions
            .AsNoTracking()
            .Where(ws =>
                ws.SessionId == request.SessionId &&
                ws.UserId == request.UserId)
            .Select(ws => new WorkoutSessionDto
            {
                SessionId = ws.SessionId,
                UserId = ws.UserId,
                Status = ws.Status
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}