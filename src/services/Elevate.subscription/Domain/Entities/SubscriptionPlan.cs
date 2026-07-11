using System;

namespace Elevate.subscription.Domain.Entities;

public sealed class SubscriptionPlan
{
    private SubscriptionPlan() { }

    public Guid Id { get; private set; }
    // Premium Monthly, VIP Annual
    public string Name { get; private set; } = string.Empty; 
    public decimal Price { get; private set; }
    public int DurationInDays { get; private set; }

    public static SubscriptionPlan Create(string name, decimal price, int durationInDays)
    {
        if (price < 0 || durationInDays <= 0)
            throw new ArgumentException("Invalid plan metrics configuration.");

        return new SubscriptionPlan
        {
            Id = Guid.NewGuid(),
            Name = name,
            Price = price,
            DurationInDays = durationInDays
        };
    }
}