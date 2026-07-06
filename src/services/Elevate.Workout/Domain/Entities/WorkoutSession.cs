using System;
using System.Collections.Generic;

namespace Elevate.Workout.Domain.Entities;

public sealed class WorkoutSession 
{
    private readonly List<WorkoutExercise> _exercises = new();

    private WorkoutSession() { }
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Type { get; private set; } = string.Empty;
    public int DurationInMinutes { get; private set; }
    public int CaloriesBurned { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Read-only navigation property exposing the encapsulated exercises list safely
    public IReadOnlyCollection<WorkoutExercise> Exercises => _exercises.AsReadOnly();

    // Factory Method to handle controlled creation of a valid domain entity instance
    public static WorkoutSession Create(Guid userId, string name, string type, int duration, int calories, DateTime createdAt)
    {
        return new WorkoutSession
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = name,
            Type = type,
            DurationInMinutes = duration,
            CaloriesBurned = calories,
            CreatedAt = createdAt
        };
    }

    // Domain Business Action: Safely append a new exercise to this specific workout session
    public void AddExercise(string exerciseName, int order)
    {
        var exercise = WorkoutExercise.Create(Id, exerciseName, order);
        _exercises.Add(exercise);

        // Audit log or domain event logic could be dispatched here in the future
    }
}