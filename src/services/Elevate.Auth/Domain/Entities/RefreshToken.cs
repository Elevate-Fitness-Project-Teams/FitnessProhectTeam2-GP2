namespace Elevate.Auth.Domain.Entities;


public class RefreshToken
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string TokenHash { get; private set; } = string.Empty;
    public DateTime ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }

    public bool IsExpired(DateTime now) => now >= ExpiresAt;
    public bool IsRevoked => RevokedAt.HasValue;
    public bool IsActive(DateTime now) => !IsRevoked && !IsExpired(now);

    private RefreshToken() { }

    internal static RefreshToken Create(Guid userId, string tokenHash, DateTime expiresAt, DateTime now) =>
        new()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            TokenHash = tokenHash,
            ExpiresAt = expiresAt,
            CreatedAt = now
        };

    internal void Revoke(DateTime now) => RevokedAt = now;
}