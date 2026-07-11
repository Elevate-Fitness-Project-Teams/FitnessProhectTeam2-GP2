namespace Elevate.Profile.Domain.ValueObjects
{
    public sealed class FullName
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public FullName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));
            if (firstName.Length > 50)
                throw new ArgumentException("First name cannot exceed 50 characters.", nameof(firstName));
            if (lastName.Length > 50)
                throw new ArgumentException("Last name cannot exceed 50 characters.", nameof(lastName));

            FirstName = firstName;
            LastName = lastName;

        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
