namespace Elevate.Auth.Infrastructure.Presistence.Interfaces
{
    // <summary>
    /// Generates and verifies 6-digit OTP codes.
    /// </summary>
    public interface IOtpService
    {
        (string PlainCode, string CodeHash) GenerateOtp();

        bool VerifyOtp(string plainCode, string storedHash);
    }
}
