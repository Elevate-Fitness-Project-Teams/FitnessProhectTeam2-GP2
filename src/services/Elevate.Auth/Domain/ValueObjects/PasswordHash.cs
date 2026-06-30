namespace AuthService.Domain.ValueObjects;

public sealed record PasswordHash
{
    public string Value { get; }

    public PasswordHash(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("Password hash cannot be empty.", nameof(hash));

        Value = hash;
    }

    public static implicit operator string(PasswordHash ph) => ph.Value;
    public override string ToString() => "[REDACTED]";
}