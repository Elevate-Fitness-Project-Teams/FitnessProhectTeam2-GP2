using Elevate.subscription.Domain.Entities;
using Elevate.subscription.Domain.Erros;
using Elevate.subscription.Infrastructure.Common.Interfaces;
using Elevate.Subscription.Domain.Enums;
using Elevate.Subscription.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Elevate.subscription.Features.UpgradeSubscription
{
    public sealed class UpgradeSubscriptionCommandHandler
        : IRequestHandler<UpgradeSubscriptionCommand,Result<UpgradeSubscriptionResponse>>
    {
        private readonly SubscriptionDbContext _db;
        private readonly IBillingSimulator _billingSimulator;

        public UpgradeSubscriptionCommandHandler(SubscriptionDbContext db, IBillingSimulator billingSimulator)
        {
            _db = db;
            _billingSimulator = billingSimulator;
        }

        public async Task<Result<UpgradeSubscriptionResponse>> Handle(
            UpgradeSubscriptionCommand request, CancellationToken ct)
        {
            var planTier = Enum.Parse<SubscriptionTier>(request.PlanTier, ignoreCase: true);

            var authorized = await _billingSimulator.AuthorizeAsync(request.UserId, planTier, request.DurationMonths, ct);

            if (!authorized)
            {
                _db.BillingLogs.Add(BillingLog.Failure(
                    request.UserId, planTier, request.DurationMonths, "Simulated billing authorization declined."));
                await _db.SaveChangesAsync(ct);

                //  failed billing → 500, not 402/409.
                return Result.Failure<UpgradeSubscriptionResponse>(SubscriptionErrors.BillingFailed);
            }

            var subscription = await _db.UserSubscriptions
                .FirstOrDefaultAsync(s => s.UserId == request.UserId, ct);

            if (subscription is null)
            {
                subscription = UserSubscription.CreatePremium(request.UserId, request.DurationMonths);
                _db.UserSubscriptions.Add(subscription);
            }
            else
            {
                subscription.UpgradeToPremium(request.DurationMonths);
            }

            _db.BillingLogs.Add(BillingLog.Success(request.UserId, subscription.Id, planTier, request.DurationMonths));

            
            await _db.SaveChangesAsync(ct);

            return Result<UpgradeSubscriptionResponse>.Success(
                new UpgradeSubscriptionResponse(subscription.Tier.ToString(), subscription.ExpiresAt!.Value));
        }
    }
}
