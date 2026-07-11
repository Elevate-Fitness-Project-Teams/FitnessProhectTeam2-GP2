namespace Elevate.Auth.Infrastructure.Presistence.Interfaces
{
    public interface ITokenService
    {
        
        (string Jwt, string RawRefreshToken, string RefreshTokenHash, DateTime ExpiresAt)
        GenerateTokenPair(Guid userId);

        (string RawToken, string TokenHash) GenerateResetToken();

        string HashRefreshToken(string rawToken);
        string HashResetToken(string rawToken);
    }
}
