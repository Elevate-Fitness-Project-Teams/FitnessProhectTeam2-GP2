using Elevate.Workout.Domain.Entities;
using Elevate.Workout.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Elevate.Workout.Infrastructure.Presistence.Seed
{
    public static class WorkoutDbContextSeeder
    {
        public static async Task SeedAsync(WorkOutDbContext db)
        {
            if (!await db.Exercises.AnyAsync())
                await SeedExercisesAsync(db);

            if (!await db.WorkoutPlans.AnyAsync())
                await SeedWorkoutPlansAndWorkoutsAsync(db);
        }

        private static async Task SeedExercisesAsync(WorkOutDbContext db)
        {
            var exercises = new[]
            {
            Exercise.Create("Push-Up", "Chest, Triceps, Shoulders", "Bodyweight",
                "Classic bodyweight pressing movement performed from a plank position.",
                "https://videos.elevatefitness.app/exercises/push-up.mp4"),
            Exercise.Create("Barbell Bench Press", "Chest, Triceps, Shoulders", "Barbell, Bench",
                "Horizontal press performed lying on a flat bench.",
                "https://videos.elevatefitness.app/exercises/bench-press.mp4"),
            Exercise.Create("Pull-Up", "Back, Biceps", "Pull-Up Bar",
                "Vertical pulling movement targeting the lats and upper back.",
                "https://videos.elevatefitness.app/exercises/pull-up.mp4"),
            Exercise.Create("Bent-Over Barbell Row", "Back, Biceps", "Barbell",
                "Hip-hinged rowing movement for back thickness.",
                "https://videos.elevatefitness.app/exercises/barbell-row.mp4"),
            Exercise.Create("Barbell Back Squat", "Legs, Glutes", "Barbell, Squat Rack",
                "Compound lower-body movement targeting quads and glutes.",
                "https://videos.elevatefitness.app/exercises/back-squat.mp4"),
            Exercise.Create("Walking Lunge", "Legs, Glutes", "Dumbbells",
                "Alternating single-leg lower-body movement.",
                "https://videos.elevatefitness.app/exercises/walking-lunge.mp4"),
            Exercise.Create("Overhead Shoulder Press", "Shoulders, Triceps", "Dumbbells",
                "Vertical pressing movement for shoulder development.",
                "https://videos.elevatefitness.app/exercises/shoulder-press.mp4"),
            Exercise.Create("Lateral Raise", "Shoulders", "Dumbbells",
                "Isolation movement targeting the lateral deltoid.",
                "https://videos.elevatefitness.app/exercises/lateral-raise.mp4"),
            Exercise.Create("Plank", "Stomach, Core", "Bodyweight",
                "Isometric core hold maintaining a straight line from shoulders to ankles.",
                "https://videos.elevatefitness.app/exercises/plank.mp4"),
            Exercise.Create("Hanging Leg Raise", "Stomach, Core", "Pull-Up Bar",
                "Core movement raising the legs while hanging from a bar.",
                "https://videos.elevatefitness.app/exercises/leg-raise.mp4"),
            Exercise.Create("Dumbbell Bicep Curl", "Arms", "Dumbbells",
                "Isolation movement for the biceps.",
                "https://videos.elevatefitness.app/exercises/bicep-curl.mp4"),
            Exercise.Create("Triceps Dip", "Arms", "Parallel Bars",
                "Compound pressing movement targeting the triceps.",
                "https://videos.elevatefitness.app/exercises/triceps-dip.mp4"),
        };

            await db.Exercises.AddRangeAsync(exercises);
            await db.SaveChangesAsync();
        }

        private static async Task SeedWorkoutPlansAndWorkoutsAsync(WorkOutDbContext db)
        {
            var difficultyNames = Enum.GetNames<DifficultyLevel>();
            var categories = Enum.GetValues<WorkoutCategory>();
            var difficulties = Enum.GetValues<DifficultyLevel>();
            var exercises = await db.Exercises.OrderBy(e => e.Id).ToListAsync();

            var planBlueprints = new[]
            {
            ("Full Body Foundations", "A balanced full-body program for building baseline strength.", 4),
            ("Upper/Lower Split", "A four-day upper/lower split for intermediate lifters.", 6),
            ("Core & Conditioning", "A core-focused program layered with conditioning work.", 3),
        };

            var plans = new List<WorkoutPlan>();
            for (var i = 0; i < planBlueprints.Length; i++)
            {
                var (name, description, weeks) = planBlueprints[i];
                var difficultyName = difficultyNames[i % difficultyNames.Length];
                plans.Add(WorkoutPlan.Create(name, description, difficultyName, weeks));
            }

            await db.WorkoutPlans.AddRangeAsync(plans);
            await db.SaveChangesAsync(); 

            var workoutLabels = new[] { "Warm-Up & Mobility", "Main Strength Block", "Accessory Work", "Finisher" };
            var categoryCursor = 0;
            var newWorkouts = new List<Domain.Entities.Workout>();

            foreach (var plan in plans)
            {
                for (var w = 0; w < workoutLabels.Length; w++)
                {
                    var workout = new Domain.Entities.Workout
                    {
                        Name = $"{plan.Name} - {workoutLabels[w]}",
                        Category = categories[categoryCursor % categories.Length],
                        Difficulty = difficulties[w % difficulties.Length],
                        EstimatedDurationInMinutes = 30 + (w * 10),
                        OrderIndex = w,
                        WorkoutPlanId = plan.Id
                    };

                    plan.Workouts.Add(workout);
                    newWorkouts.Add(workout);
                    categoryCursor++;
                }
            }

            await db.SaveChangesAsync(); 

            var exerciseCursor = 0;
            foreach (var workout in newWorkouts)
            {
                var exerciseCountForWorkout = 3 + (workout.OrderIndex % 2); 
                for (var e = 0; e < exerciseCountForWorkout; e++)
                {
                    var exercise = exercises[exerciseCursor % exercises.Count];
                    workout.AddExercise(exercise.Id, e);
                    exerciseCursor++;
                }
            }

            await db.SaveChangesAsync();
        }
    }
}
