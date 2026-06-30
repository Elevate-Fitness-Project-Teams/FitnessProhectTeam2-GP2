public sealed record FullName
{
    public string FirstName { get; }
    public string LastName { get; }
    public string DisplayName => $"{FirstName} {LastName}";

    public FullName(string firstName, string lastName)
    {
        firstName = firstName?.Trim() ?? string.Empty;
        lastName = lastName?.Trim() ?? string.Empty;

        if (firstName.Length is < 2 or > 50)
            throw new ArgumentException("First name must be between 2 and 50 characters.", nameof(firstName));

        if (lastName.Length is < 2 or > 50)
            throw new ArgumentException("Last name must be between 2 and 50 characters.", nameof(lastName));

        FirstName = firstName;
        LastName = lastName;
    }
}