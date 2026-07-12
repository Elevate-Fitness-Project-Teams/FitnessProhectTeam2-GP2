namespace Elevate.Progress.Features.LogWorkoutCompletion.DTOS
{
    public class UpdateUserStatisticsRequestDto
    {
        public Guid UserId { get; set; }

        public int Duration { get; set; }

        public int CaloriesBurned { get; set; }
    }
}
