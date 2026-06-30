namespace AuthService.Domain.Exceptions;

public sealed class EmailAlreadyExistsException(string email)
    : Exception($"Email '{email}' is already registered.");  

public sealed class InvalidCredentialsException()
    : Exception("Invalid email or password."); 

public sealed class AccountLockedException(DateTime lockedUntil)
    : Exception($"Account is locked until {lockedUntil:O}."); 23

public sealed class ProfileAlreadyCompletedException(Guid userId)
    : Exception($"User '{userId}' has already completed profile setup.");

public sealed class InvalidOtpException()
    : Exception("The OTP code is invalid."); // AUTH_INVALID_OTP → 400

public sealed class OtpExpiredException()
    : Exception("The OTP code has expired."); // AUTH_OTP_EXPIRED → 400

public sealed class OtpResendTooSoonException(int secondsRemaining)
    : Exception($"Please wait {secondsRemaining}s before requesting a new code."); // RATE_OTP_RESEND_TOO_SOON → 429

public sealed class InvalidResetTokenException()
    : Exception("The reset token is invalid, expired, or already used."); // AUTH_RESET_TOKEN_INVALID → 400

public sealed class TokenInvalidException()
    : Exception("The token is missing or invalid."); // AUTH_TOKEN_INVALID → 401

public sealed class TokenExpiredException()
    : Exception("The token has expired."); // AUTH_TOKEN_EXPIRED → 401

public sealed class PasswordMismatchException()
    : Exception("The passwords do not match."); // AUTH_PASSWORD_MISMATCH → 400

public sealed class WeakPasswordException()
    : Exception("Password must be at least 6 characters with 1 uppercase letter and 1 number."); // AUTH_WEAK_PASSWORD → 400