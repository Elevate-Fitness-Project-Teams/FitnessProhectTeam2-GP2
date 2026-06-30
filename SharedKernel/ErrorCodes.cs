using System;

namespace SharedKernel;

//  (Auth & Identity)
public enum AuthError
{
    EmailExists,
    WeakPassword,
    InvalidCredentials,
    AccountLocked,
    InvalidOtp,
    OtpExpired,
    PasswordMismatch,
    ResetTokenInvalid,
    TokenInvalid,
    TokenExpired
}

public enum ValidationError
{
    RequiredField,
    InvalidAge,
    InvalidWeight,
    InvalidHeight,
    InvalidGender,
    InvalidGoal,
    InvalidActivity,
    InvalidFileType,
    FileTooLarge,
    InvalidDate
}
 //(Fitness/Calculation Engine - FCE)
public enum FceError
{
    StatsNotFound,
    InvalidCalculation,
    MetricsNotCalculated,
    NoMatchingPlan
}

public enum ResourceError
{
    NotFound,
    WorkoutNotFound,
    PlanNotFound,
    ExerciseNotFound,
    SessionNotFound,
    MealNotFound,
    UserNotFound
}

// (Permissions & Rate Limits)
public enum RateError
{
    OtpResendTooSoon,
    PremiumRequired,
    RateLimitExceeded
}

public enum ServerError
{
    FileUploadFailed,
    ServiceUnavailable,
    DatabaseError
}