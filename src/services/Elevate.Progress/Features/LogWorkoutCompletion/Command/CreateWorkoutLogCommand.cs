using Elevate.Progress.Domain.Entities;
using Elevate.Progress.Features.LogWorkoutCompletion.DTOS;
using Elevate.Progress.Infrastructure.Persistence.ProgressDbContext;
using MediatR;

namespace Elevate.Progress.Features.LogWorkoutCompletion.Command;

public record CreateWorkoutLogCommand(CreateWorkoutLogRequestDto Request)
    : IRequest<WorkoutLogResponseDto>;

public class CreateWorkoutLogCommandHandler
    : IRequestHandler<CreateWorkoutLogCommand, WorkoutLogResponseDto>
{
    private readonly ProgressDbContext _context;

    public CreateWorkoutLogCommandHandler(ProgressDbContext context)
    {
        _context = context;
    }

    public async Task<WorkoutLogResponseDto> Handle(
        CreateWorkoutLogCommand request,
        CancellationToken cancellationToken)
    {
        var workoutLog = new WorkoutLog
        {
            UserId = request.Request.UserId,
            WorkoutId = request.Request.WorkoutId,
            WorkoutSessionId = request.Request.WorkoutSessionId,
            CompletedAt = request.Request.CompletedAt,
            Duration = request.Request.Duration,
            CaloriesBurned = request.Request.CaloriesBurned,
            Difficulty = request.Request.Difficulty,
            Notes = request.Request.Notes,
            Rating = request.Request.Rating
        };

        _context.WorkoutLogs.Add(workoutLog);

        await _context.SaveChangesAsync(cancellationToken);

        return new WorkoutLogResponseDto
        {
            WorkoutLogId = workoutLog.Id
        };
    }
}