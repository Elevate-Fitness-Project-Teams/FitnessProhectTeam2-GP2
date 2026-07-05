namespace Elevate.Auth.Features.Login
{
    public sealed record LoginResponse(
    string Token,
    string RefreshToken,
    bool ProfileCompleted,
    bool IsPremium,
    string FirstName,
    string LastName,
    string Email);
}
