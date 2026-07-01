namespace Elevate.Auth.Domain.Errors;

public static class AuthErrorCodes
{
    //  Identity 
    public const string EmailExists          = "AUTH_EMAIL_EXISTS";           // 409
    public const string WeakPassword         = "AUTH_WEAK_PASSWORD";          // 400
    public const string InvalidCredentials   = "AUTH_INVALID_CREDENTIALS";    // 401
    public const string AccountLocked        = "AUTH_ACCOUNT_LOCKED";         // 423
    public const string InvalidOtp           = "AUTH_INVALID_OTP";            // 400
    public const string OtpExpired           = "AUTH_OTP_EXPIRED";            // 400
    public const string PasswordMismatch     = "AUTH_PASSWORD_MISMATCH";      // 400
    public const string ResetTokenInvalid    = "AUTH_RESET_TOKEN_INVALID";    // 400
    public const string TokenInvalid         = "AUTH_TOKEN_INVALID";          // 401
    public const string TokenExpired         = "AUTH_TOKEN_EXPIRED";          // 401

    // Rate limiting 
    public const string OtpResendTooSoon     = "RATE_OTP_RESEND_TOO_SOON";    // 429
}
