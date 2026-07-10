namespace Elevate.Auth.Features.RefreshToken
{
    public sealed record RefreshTokenResponse(
    string NewAccessToken,
    string NewRefreshToken);
}
