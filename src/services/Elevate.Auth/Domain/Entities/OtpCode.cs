namespace Elevate.Auth.Domain.Entities;

/// <summary>
/// Child entity owned by User.
/// Spec 1.4: 6-digit code stored as hash. TTL = 10 minutes. Resend cooldown = 30 seconds.
/// Spec 1.5: On verify → AttachResetToken(). Short-lived, single-use reset token issued.
/// Spec 1.6: ResetToken consumed on password reset → IsUsed = true.
/// </summary>
public class OtpCode
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }

    /// <summary>Hashed 6-digit code. Never stored plain.</summary>
    public string CodeHash { get; private set; } = string.Empty;

    /// <summary>Attached after OTP verified. Hashed. Single-use.</summary>
    public string? ResetTokenHash { get; private set; }

    public DateTime ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsUsed { get; private set; }

    public bool IsExpired(DateTime now) => now >= ExpiresAt;

    /// <summary>Spec 1.4: 30-second resend cooldown.</summary>
    public bool IsWithinResendCooldown(DateTime now) =>
        now < CreatedAt.AddSeconds(30);

    private OtpCode() { }

    internal static OtpCode Create(Guid userId, string codeHash, DateTime expiresAt, DateTime now) =>
        new()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CodeHash = codeHash,
            ExpiresAt = expiresAt,
            CreatedAt = now
        };

    internal void MarkUsed() => IsUsed = true;

    /// <summary>
    /// Called after successful OTP verification.
    /// Attaches the reset token hash and marks OTP as consumed.
    /// </summary>
    public void AttachResetToken(string resetTokenHash)
    {
        ResetTokenHash = resetTokenHash;
        IsUsed = true;
    }
}