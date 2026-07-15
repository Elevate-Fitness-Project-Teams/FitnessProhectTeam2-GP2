namespace Elevate.Profile.Domain.ValueObjects
{
    public sealed class Email
    {
        public string Value { get; private set; } = null!;

        private Email() { } // EF Core

        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            if (!email.Contains("@"))
                throw new ArgumentException("Invalid email format.", nameof(email));
            Value = email;

        }
        public override string ToString() => Value;
    }
}
