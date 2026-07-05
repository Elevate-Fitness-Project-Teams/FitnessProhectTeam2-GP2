using SharedKernel;

namespace Elevate.Auth.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    private Email(string value) 
    {
        Value = value;
    }

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<Email>(new Error("AUTH_EMAIL_REQUIRED", "Email cannot be empty.", ErrorType.Validation));

        var processedValue = value.Trim().ToLowerInvariant();
        return Result.Success(new Email(processedValue));
    }

    public static implicit operator string(Email e) => e.Value;
    public override string ToString() => Value;
}