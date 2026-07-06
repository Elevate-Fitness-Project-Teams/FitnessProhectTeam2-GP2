using System;

namespace Elevate.Workout.Domain.Entities;

public sealed class ExerciseSet 
{
    private ExerciseSet() { }
    public Guid Id { get; private set; }
    public Guid WorkoutExerciseId { get; private set; }
    public int SetNumber { get; private set; }
    public int Reps { get; private set; }
    public decimal WeightInKg { get; private set; }
    public bool IsCompleted { get; private set; }

    internal static ExerciseSet Create(Guid workoutExerciseId, int setNumber, int reps, decimal weightInKg)
    {
        if (reps < 0 || weightInKg < 0)
        {
            throw new ArgumentException("Metrics data arguments cannot be negative numerical values.");
        }

        return new ExerciseSet
        {
            Id = Guid.NewGuid(),
            WorkoutExerciseId = workoutExerciseId,
            SetNumber = setNumber,
            Reps = reps,
            WeightInKg = weightInKg,
            IsCompleted = false 
        };
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }
}