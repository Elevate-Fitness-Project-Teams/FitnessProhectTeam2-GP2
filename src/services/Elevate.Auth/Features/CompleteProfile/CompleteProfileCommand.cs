using MediatR;
using SharedKernel;

namespace Elevate.Auth.Features.CompleteProfile;

public sealed record CompleteProfileCommand(
    Guid UserId ): IRequest<Result<CompleteProfileResponse>>;
