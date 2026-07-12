namespace Elevate.Progress.Integration.RabbitMQ
{
    public static class RabbitMqConstants
    {
        public const string Exchange = "fitness.events";

        public static class RoutingKeys
        {
            public const string WeightUpdated = "weight.updated";
            public const string AchievementEarned = "achievement.earned";
        }
    }
}
