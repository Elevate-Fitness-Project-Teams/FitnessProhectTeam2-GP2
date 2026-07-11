namespace Elevate.Auth.Features.ForgotPassword
{
    public sealed record ForgotPasswordResponse(
    int OtpExpiresIn,   // seconds (600 = 10 min)
    int CanResendIn);   // seconds (30
}
