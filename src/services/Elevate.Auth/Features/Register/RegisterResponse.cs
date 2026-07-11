namespace Elevate.Auth.Features.Register
{
    public sealed record RegisterResponse(
    Guid UserId,
    string Email,
    bool RequiresProfileCompletion);
}
