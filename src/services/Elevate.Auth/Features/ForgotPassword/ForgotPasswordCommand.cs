using MediatR;
using SharedKernel;

namespace Elevate.Auth.Features.ForgotPassword;

public sealed record ForgotPasswordCommand(string Email) : IRequest<Result<ForgotPasswordResponse>>;
