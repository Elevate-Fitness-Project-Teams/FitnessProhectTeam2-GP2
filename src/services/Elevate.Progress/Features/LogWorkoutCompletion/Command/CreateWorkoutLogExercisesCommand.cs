using Elevate.Progress.Domain.Entities;
using Elevate.Progress.Features.LogWorkoutCompletion.DTOS;
using Elevate.Progress.Infrastructure.Persistence;
using MediatR;

namespace Elevate.Progress.Features.LogWorkoutCompletion.Command;

public record CreateWorkoutLogExercisesCommand(
    Guid WorkoutLogId,
    List<WorkoutLogExercisesDto> Exercises
) : IRequest<WorkoutLogExercisesResponseDto>;

public class CreateWorkoutLogExercisesCommandHandler
    : IRequestHandler<CreateWorkoutLogExercisesCommand, WorkoutLogExercisesResponseDto>
{
    private readonly ProgressDbContext _context;

    public CreateWorkoutLogExercisesCommandHandler(ProgressDbContext context)
    {
        _context = context;
    }

    public async Task<WorkoutLogExercisesResponseDto> Handle(
        CreateWorkoutLogExercisesCommand request,
        CancellationToken cancellationToken)
    {
        var workoutLogExercises = request.Exercises
            .Select(exercise => new WorkoutLogExercises
            {
                WorkoutLogId = request.WorkoutLogId,
                ExerciseId = exercise.ExerciseId
            })
            .ToList();

        _context.WorkoutLogExercises.AddRange(workoutLogExercises);

        await _context.SaveChangesAsync(cancellationToken);

        return new WorkoutLogExercisesResponseDto
        {
            Success = true
        };
    }
}
