namespace Elevate.Auth.Domain.Entities;


public class LoginAttempt
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public bool IsSuccess { get; private set; }
    public string IpAddress { get; private set; } = string.Empty;
    public DateTime AttemptedAt { get; private set; }

    private LoginAttempt() { }

    internal static LoginAttempt Create(Guid userId, bool isSuccess, string ipAddress, DateTime now) =>
        new()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            IsSuccess = isSuccess,
            IpAddress = ipAddress,
            AttemptedAt = now
        };
}