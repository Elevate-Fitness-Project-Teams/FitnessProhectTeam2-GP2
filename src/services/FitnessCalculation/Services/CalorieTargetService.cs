using System.Collections.Concurrent;
using Elevate.Fitness.Models;

namespace Elevate.Fitness.Services;

public class CalorieTargetService
{
    private readonly ConcurrentDictionary<int, CalorieTarget> _store = new();

    public CalorieTargetService()
    {
        _store.TryAdd(1, new CalorieTarget
        {
            UserId = 1,
            TargetCalories = 2200,
            CalculatedAt = DateTime.UtcNow
        });

        _store.TryAdd(2, new CalorieTarget
        {
            UserId = 2,
            TargetCalories = 1800,
            CalculatedAt = DateTime.UtcNow
        });
    }

    public Task<CalorieTarget?> GetByUserIdAsync(int userId)
    {
        _store.TryGetValue(userId, out var target);
        return Task.FromResult(target);
    }
}
