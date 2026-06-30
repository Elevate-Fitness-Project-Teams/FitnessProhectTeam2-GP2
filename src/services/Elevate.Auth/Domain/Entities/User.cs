
using AuthService.Domain.ValueObjects;
using Elevate.Auth.Domain.DomainEvents;
using Elevate.Auth.Domain.Enums;
using Elevate.Auth.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using SharedKernel;

namespace Elevate.Auth.Domain.Entities;
public class User  : BaseEntity
{
    public Guid Id { get; private set; }
    public FullName Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public PasswordHash PasswordHash { get; private set; } = null!;

    public bool RequiresProfileCompletion { get; private set; } = true;
    public bool IsLockedOut { get; private set; }
    public DateTime? LockedUntil { get; private set; }
    public bool IsActive { get; private set; } = true;
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<LoginAttempt> _loginAttempts = [];
    private readonly List<RefreshToken> _refreshTokens = [];
    private readonly List<OtpCode> _otpCodes = [];

    public IReadOnlyCollection<LoginAttempt> LoginAttempts => _loginAttempts.AsReadOnly();
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();
    public IReadOnlyCollection<OtpCode> OtpCodes => _otpCodes.AsReadOnly();

    private User()
    {

    }

    public static User Register(
        FullName name,
        Email email,
        string phoneNumber,
        PasswordHash passwordHash,
        DateTime now)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            PasswordHash = passwordHash,
            RequiresProfileCompletion = true,
            IsLockedOut = false,
            IsActive = true,
            CreatedAt = now
        };

        user.AddDomainEvent(new UserRegisteredEvent(user.Id, email, now));
        return user;
    }

    /// to create UserProfiles + UserPreferences rows via RabbitMQ outbox.
    /// </summary>
    public void CompleteProfile(DateTime now)
    {
        RequiresProfileCompletion = false;
        UpdatedAt = now;
        AddDomainEvent(new UserProfileCompletedEvent(Id, now));
    }

    /// <summary>
    /// Spec 1.3: Record login attempt and enforce lockout.
    /// Rule: 5 failures within 15 minutes → IsLockedOut = true, LockedUntil = now + 15 min.
    /// </summary>
    public LoginAttemptResult RecordLoginAttempt(bool success, string ipAddress, DateTime now)
    {
        var attempt = LoginAttempt.Create(Id, success, ipAddress, now);
        _loginAttempts.Add(attempt);

        if (success)
        {
            IsLockedOut = false;
            LockedUntil = null;
            UpdatedAt = now;
            return LoginAttemptResult.Success;
        }

        var windowStart = now.AddMinutes(-15);
        var recentFailures = _loginAttempts
            .Count(a => !a.IsSuccess && a.AttemptedAt >= windowStart);

        if (recentFailures >= 5)
        {
            IsLockedOut = true;
            LockedUntil = now.AddMinutes(15);
            UpdatedAt = now;
            AddDomainEvent(new UserLockedOutEvent(Id, LockedUntil.Value, now));
            return LoginAttemptResult.LockedOut;
        }

        return LoginAttemptResult.InvalidCredentials;
    }

    public bool IsCurrentlyLockedOut(DateTime now) =>
        IsLockedOut && LockedUntil.HasValue && LockedUntil.Value > now;

    /// <summary>
    /// Spec 1.7: Issue refresh token. Rotate-on-use — caller must revoke old token first.
    /// </summary>
    public RefreshToken IssueRefreshToken(string tokenHash, DateTime expiresAt, DateTime now)
    {
        var token = RefreshToken.Create(Id, tokenHash, expiresAt, now);
        _refreshTokens.Add(token);
        return token;
    }

   
    public void RevokeAllRefreshTokens(DateTime now)
    {
        foreach (var token in _refreshTokens.Where(t => t.RevokedAt is null))
            token.Revoke(now);

        UpdatedAt = now;
    }


    public void ResetPassword(PasswordHash newHash, DateTime now)
    {
        PasswordHash = newHash;
        RevokeAllRefreshTokens(now);
        UpdatedAt = now;
        AddDomainEvent(new PasswordResetEvent(Id, now));
    }

    
    public OtpCode IssueOtpCode(string codeHash, DateTime expiresAt, DateTime now)
    {
        foreach (var old in _otpCodes.Where(o => !o.IsUsed))
            old.MarkUsed();

        var otp = OtpCode.Create(Id, codeHash, expiresAt, now);
        _otpCodes.Add(otp);
        return otp;
    }

    public OtpCode? GetActiveOtpCode(DateTime now) =>
        _otpCodes
            .Where(o => !o.IsUsed && !o.IsExpired(now))
            .OrderByDescending(o => o.CreatedAt)
            .FirstOrDefault();

    public OtpCode? GetLatestOtpCode() =>
        _otpCodes
            .OrderByDescending(o => o.CreatedAt)
            .FirstOrDefault();
}

