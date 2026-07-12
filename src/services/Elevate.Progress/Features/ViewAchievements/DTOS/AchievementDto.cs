namespace Elevate.Progress.Features.ViewAchievements.DTOS
{
    public class AchievementDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string IconUrl { get; set; } = default!;
    }
}
