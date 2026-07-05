using MediatR;
using SharedKernel;

namespace Elevate.Auth.Features.ResetPassword;

/// Caller holds a short-lived resetToken issued by VerifyOtp.
public sealed record ResetPasswordCommand(
    string ResetToken,
    string NewPassword) : IRequest<Result<ResetPasswordResponse>>;

