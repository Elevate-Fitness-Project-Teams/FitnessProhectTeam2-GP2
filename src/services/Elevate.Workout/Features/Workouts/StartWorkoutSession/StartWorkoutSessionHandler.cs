using Elevate.Workout.Domain.Entities;
using Elevate.Workout.Domain.Enums;
using Elevate.Workout.Features.Workouts.StartWorkoutSession;
using Elevate.Workout.Infrastructure.Presistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

public class StartWorkoutSessionHandler : IRequestHandler<StartWorkoutSessionCommand, Result<StartWorkoutSessionResponse>>
{
    private readonly WorkOutDbContext _context;
    private static readonly Guid CurrentUserId = Guid.Parse("00000000-0000-0000-0000-000000000001");

    public StartWorkoutSessionHandler(WorkOutDbContext context)
    {
        _context = context;
    }

    public async Task<Result<StartWorkoutSessionResponse>> Handle(StartWorkoutSessionCommand request, CancellationToken cancellationToken)
    {
        var originalWorkoutData = await _context.Workouts
            .Where(w => w.WorkoutId == request.WorkoutId)
            .Select(w => new
            {
                w.WorkoutId,
                w.Name,
                Exercises = w.Exercises.Select(we => new { we.ExerciseId, we.OrderIndex }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (originalWorkoutData == null)
        {
            return Result.Failure<StartWorkoutSessionResponse>(new Error(
                Code: "RES_WORKOUT_NOT_FOUND",
                Message: $"Workout with ID {request.WorkoutId} was not found.",
                Type: ErrorType.NotFound
            ));
        }

        var existingActiveSessionData = await _context.WorkoutSessions
            .Where(s => s.WorkoutID == request.WorkoutId && s.UserId == CurrentUserId && s.Type == SessionStatus.Active)
            .Select(s => new
            {
                     s.Id,
                     Exercises = s.Exercises
                    .OrderBy(e => e.Order)
                    .Select(e => new SessionExerciseResponse(
                        e.Id,
                        e.Exercise.Name, 
                        e.Order))
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (existingActiveSessionData != null)
        {
            return Result.Success(new StartWorkoutSessionResponse(existingActiveSessionData.Id, existingActiveSessionData.Exercises));
        }

        var newSession = WorkoutSession.Create(
            userId: CurrentUserId,
            name: $"{originalWorkoutData.Name} Session",
            type: SessionStatus.Active,
            duration: request.PlannedDuration,
            calories: 0,
            createdAt: DateTime.UtcNow,
            workoutId: originalWorkoutData.WorkoutId
        );

        foreach (var originalExercise in originalWorkoutData.Exercises.OrderBy(e => e.OrderIndex))
        {
            newSession.AddExercise(originalExercise.ExerciseId, originalExercise.OrderIndex);
        }

        _context.WorkoutSessions.Add(newSession);
        await _context.SaveChangesAsync(cancellationToken);

        var sessionExercises = await _context.WorkoutExercises
            .Where(we => we.WorkoutSessionId == newSession.Id)
            .OrderBy(we => we.Order)
            .Select(we => new SessionExerciseResponse(
                we.Id,
                we.Exercise.Name, 
                we.Order))
            .ToListAsync(cancellationToken);

        return Result.Success(new StartWorkoutSessionResponse(newSession.Id, sessionExercises));
    }
}
