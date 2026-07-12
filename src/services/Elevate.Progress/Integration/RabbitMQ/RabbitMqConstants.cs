namespace Elevate.Progress.Integration.RabbitMQ
{
    public static class RabbitMqConstants
    {
        public const string FitnessExchange = "fitness.events";
        public const string ProgressExchange = "progress.exchange";

        public static class RoutingKeys
        {
            public const string WeightUpdated = "weight.updated";
            public const string AchievementEarned = "achievement.earned";
        }
    }
}
