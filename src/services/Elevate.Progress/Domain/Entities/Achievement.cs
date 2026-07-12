using Elevate.Progress.Domain.Enums;

namespace Elevate.Progress.Domain.Entities
{
    public class Achievement
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string IconUrl { get; set; } = default!;

        public int RequiredValue { get; set; }

        public AchievementType Type { get; set; }
    }
}
