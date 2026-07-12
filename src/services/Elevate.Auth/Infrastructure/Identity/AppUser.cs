using Elevate.Auth.Domain.DomainEvents;
using Elevate.Auth.Domain.Entities;
using Elevate.Auth.Domain.Enums;
using Elevate.Auth.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Interfaces;
namespace Elevate.Auth.Infrastructure.Identity; 

public sealed class AppUser
    : IdentityUser<Guid>
{
    public FullName Name { get; private set; } = default!;

    public bool RequiresProfileCompletion { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsDeleted { get; private set; }
    public bool IsLockedOut { get; private set; }
    public DateTime? LockedUntil { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    private readonly List<LoginAttempt> _loginAttempts = [];

    private readonly List<RefreshToken> _refreshTokens = [];

    private readonly List<OtpCode> _otpCodes = [];

    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<LoginAttempt> LoginAttempts
        => _loginAttempts;

    public IReadOnlyCollection<RefreshToken> RefreshTokens
        => _refreshTokens;

    public IReadOnlyCollection<OtpCode> OtpCodes
        => _otpCodes;

    public IReadOnlyCollection<IDomainEvent> DomainEvents
        => _domainEvents;

    private AppUser() { }

    public static AppUser RegisterUser(
        FullName name,
        Email email,
        string phone,
        DateTime now)
    {
        var user = new AppUser();

        user.Id = Guid.NewGuid();

        user.Name = name;

        user.Email = email;

        user.UserName = email;

        user.PhoneNumber = phone;

        user.CreatedAt = now;

        user.RequiresProfileCompletion = true;

        user.IsActive = true;

        user.AddDomainEvent(
            new UserRegisteredEvent(user.Id, email, name.FirstName,name.LastName, phone, now));

        return user;
    }

    public void CompleteProfile(DateTime now)
    {
        RequiresProfileCompletion = false;

        UpdatedAt = now;

        AddDomainEvent(
            new UserProfileCompletedEvent(Id, now));
    }
    public bool IsCurrentlyLockedOut(DateTime now) =>
        IsLockedOut && LockedUntil.HasValue && LockedUntil.Value > now;
    public LoginAttemptResult EvaluateLockout(bool success, DateTime now)
    {
        if (success)
        {
            IsLockedOut = false;
            LockedUntil = null;
            return LoginAttemptResult.Success;
        }

        var windowStart = now.AddMinutes(-15);
        var recentFailures = _loginAttempts.Count(a => !a.IsSuccess && a.AttemptedAt >= windowStart);

        if (recentFailures >= 5)
        {
            IsLockedOut = true;
            LockedUntil = now.AddMinutes(15);
            AddDomainEvent(new UserLockedOutEvent(Id, LockedUntil.Value, now));
            return LoginAttemptResult.LockedOut;
        }

        return LoginAttemptResult.InvalidCredentials;
    }

    private void AddDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);
    public void RecordSuccessfulLogin(string refreshTokenHash, DateTime expiry, DateTime now)
    {
        LastLoginAt = now;
        UpdatedAt = now;

        var token = RefreshToken.Create(Id, refreshTokenHash, expiry, now);
        _refreshTokens.Add(token);
    }
    public void ResetPassword(DateTime now)
    {
        // PasswordHash is set by UserManager before this is called
        UpdatedAt = now;
        AddDomainEvent(new PasswordResetEvent(Id, now));
    }
    public void ClearDomainEvents()
        => _domainEvents.Clear();
}