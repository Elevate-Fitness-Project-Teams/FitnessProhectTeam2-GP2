namespace Elevate.Progress.Features.ViewProgressDashboard.DTOS
{
    public class GetEarnedAchievementsResponseDto
    {
        public List<EarnedAchievementRecordDto> Achievements { get; set; } = [];
    }

    public class EarnedAchievementRecordDto
    {
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string IconUrl { get; set; } = default!;

        public DateTime EarnedAt { get; set; }
    }
}
