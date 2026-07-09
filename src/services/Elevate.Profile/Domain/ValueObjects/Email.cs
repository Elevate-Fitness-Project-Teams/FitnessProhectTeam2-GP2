namespace Elevate.Profile.Domain.ValueObjects
{
    public sealed class Email
    {
        public string email { get; private set; } = null!;

        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            if (!email.Contains("@"))
                throw new ArgumentException("Invalid email format.", nameof(email));
            this.email = email;

        }
    }
}
