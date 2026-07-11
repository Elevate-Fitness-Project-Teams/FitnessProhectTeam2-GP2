using System;
using System.Collections.Generic;

namespace Elevate.Workout.Domain.Entities;

public sealed class WorkoutExercise 
{
    // Private backing field maintaining encapsulation over the sets collection
    private readonly List<ExerciseSet> _sets = new();

    private WorkoutExercise() { }

    public Guid Id { get; private set; }
    public Guid WorkoutSessionId { get; private set; }
    public string ExerciseName { get; private set; } = string.Empty;
    public int Order { get; private set; }

    public IReadOnlyCollection<ExerciseSet> Sets => _sets.AsReadOnly();

    // Factory Method to control the instantiation context from the parent aggregate root
    internal static WorkoutExercise Create(Guid workoutSessionId, string exerciseName, int order)
    {
        return new WorkoutExercise
        {
            Id = Guid.NewGuid(),
            WorkoutSessionId = workoutSessionId,
            ExerciseName = exerciseName,
            Order = order
        };
    }

    // Domain Business Action: Safely inject a training set configuration under this exercise record
    public void AddSet(int setNumber, int reps, decimal weightInKg)
    {
        var set = ExerciseSet.Create(Id, setNumber, reps, weightInKg);
        _sets.Add(set);
    }
}