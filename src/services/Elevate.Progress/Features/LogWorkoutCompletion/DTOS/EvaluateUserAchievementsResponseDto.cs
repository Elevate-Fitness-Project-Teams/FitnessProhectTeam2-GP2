namespace Elevate.Progress.Features.LogWorkoutCompletion.DTOS
{
    public class EvaluateUserAchievementsResponseDto
    {
        public bool Success { get; set; }
        public int NewAchievementsCount { get; set; }
        public List<Guid> NewAchievementIds { get; set; } = new();

    }
}
