namespace Elevate.Workout.Domain.Entities
{
    public sealed class Exercise
    {
        private Exercise() { } 

        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string TargetMuscles { get; private set; } = string.Empty;
        public string Equipment { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string VideoUrl { get; private set; } = string.Empty;

        public static Exercise Create(string name, string targetMuscles, string equipment, string description, string videoUrl)
        {
            return new Exercise
            {
                Name = name,
                TargetMuscles = targetMuscles,
                Equipment = equipment,
                Description = description,
                VideoUrl = videoUrl
            };
        }
    }
}
