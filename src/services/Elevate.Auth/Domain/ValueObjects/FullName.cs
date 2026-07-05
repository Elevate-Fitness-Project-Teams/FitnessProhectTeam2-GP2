using SharedKernel;

namespace Elevate.Auth.Domain.ValueObjects;

public sealed record FullName
{
    public string FirstName { get; }
    public string LastName { get; }
    public string DisplayName => $"{FirstName} {LastName}";

    private FullName(string firstName, string lastName) 
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Result<FullName> Create(string firstName, string lastName)
    {
        firstName = firstName?.Trim() ?? string.Empty;
        lastName = lastName?.Trim() ?? string.Empty;

        if (firstName.Length is < 2 or > 50)
            return Result.Failure<FullName>(new Error("AUTH_INVALID_FIRSTNAME", "First name must be between 2 and 50 characters.", ErrorType.Validation));

        if (lastName.Length is < 2 or > 50)
            return Result.Failure<FullName>(new Error("AUTH_INVALID_LASTNAME", "Last name must be between 2 and 50 characters.", ErrorType.Validation));

        return Result.Success(new FullName(firstName, lastName));
    }
}