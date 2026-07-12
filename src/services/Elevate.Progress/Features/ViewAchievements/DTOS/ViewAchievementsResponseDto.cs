namespace Elevate.Progress.Features.ViewAchievements.DTOS
{
    public class ViewAchievementsResponseDto
    {
        public List<AchievementRecordDto> Achievements { get; set; } = [];
    }

    public class AchievementRecordDto
    {
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string IconUrl { get; set; } = default!;

        public DateTime EarnedAt { get; set; }
    }
}
