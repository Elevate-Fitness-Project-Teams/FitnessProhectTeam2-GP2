namespace Elevate.Workout.Features.Exercises.GetExcersiceDetail
{
    public record ExerciseDetailResponse(
    int Id,
    string Name,
    string TargetMuscles,
    string Equipment,
    string Description,
    string VideoUrl
);
}
