namespace Elevate.Progress.Features.ViewProgressStats.DTOS
{
    public class GetStreakResponseDto
    {
        public int CurrentStreak { get; set; }

        public int LongestStreak { get; set; }
    }
}