using Elevate.Profile.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Elevate.Profile.Domain.Entities
{
    public class UserPreferences
    {
        public Guid UserId { get; set; }
        [MaxLength(10)]
        public Language Language { get; set; } = Language.en;
        [MaxLength(15)]
        public Theme Theme { get; set; } = Theme.light;
        [MaxLength(5)]
        public WeightUnit WeightUnit { get; set; } = WeightUnit.kg  ;
        [MaxLength(5)]
        public HeightUnit HeightUnit { get; set; } = HeightUnit.cm;
        [MaxLength(5)]
        public DistanceUnit DistanceUnit { get; set; } = DistanceUnit.Km;
        public UserProfile UserProfile { get; set; } = null!;
    }
}
