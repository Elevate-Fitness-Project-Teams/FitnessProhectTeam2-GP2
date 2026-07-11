using MediatR;
using SharedKernel;

namespace Elevate.Auth.Features.VerifyOtp;

public sealed record VerifyOtpCommand(
    string Email,
    string Otp) : IRequest<Result<VerifyOtpResponse>>;

