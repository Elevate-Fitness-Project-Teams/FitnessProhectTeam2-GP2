namespace Elevate.Profile.Application.Features.Profile.DTO
{
    public class GetProfileDTo
    {

        public int UserId { get; set; }

        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string? PhoneNumber { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public bool IsPremiumCached { get; set; }

        public DateTime MemberSince { get; set; }


        // Cached statistics snapshot
        public int TotalWorkouts { get; set; }

        public int CurrentStreak { get; set; }

        public int LongestStreak { get; set; }

        public int TotalCaloriesBurned { get; set; }

        public double TotalWeightLost { get; set; }



    }
}
