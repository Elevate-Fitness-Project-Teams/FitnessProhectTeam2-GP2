using AuthService.Infrastructure.Persistence;
using Elevate.Auth.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System.Threading;
using System.Threading.Tasks;

namespace Elevate.Auth.Features.Logout;

public sealed class LogoutCommandHandler(AuthDbContext db)
    : IRequestHandler<LogoutCommand, Result>
{
    public async Task<Result> Handle(LogoutCommand cmd, CancellationToken ct)
    {
        var tokenRecord = await db.RefreshTokens
            .FirstOrDefaultAsync(t => t.TokenHash == cmd.RefreshToken && t.UserId == cmd.UserId, ct);

        if (tokenRecord is null)
        {
            return Result.Failure(new Error(
                "AUTH_INVALID_TOKEN",
                "Invalid or already expired session.",
                ErrorType.Validation));
        }

        db.RefreshTokens.Remove(tokenRecord);
        await db.SaveChangesAsync(ct);

        return Result.Success();
    }
}