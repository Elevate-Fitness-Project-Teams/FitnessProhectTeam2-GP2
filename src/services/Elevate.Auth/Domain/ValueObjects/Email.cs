namespace Elevate.Auth.Domain.ValueObjects;


public sealed record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty.", nameof(value));

        Value = value.Trim().ToLowerInvariant();
    }

    public static implicit operator string(Email e) => e.Value;
    public override string ToString() => Value;
}
