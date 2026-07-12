using Elevate.subscription.Domain.Entities;
using Elevate.subscription.Domain.Erros;
using Elevate.subscription.Domain.Events;
using Elevate.subscription.Infrastructure.Common.Interfaces;
using Elevate.Subscription.Domain.Enums;
using Elevate.Subscription.Infrastructure.Persistence;
using MassTransit;
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
        private readonly IPublishEndpoint _publishEndpoint;

        public UpgradeSubscriptionCommandHandler(SubscriptionDbContext db, 
            IBillingSimulator billingSimulator,IPublishEndpoint publishEndpoint)
        {
            _db = db;
            _billingSimulator = billingSimulator;
            _publishEndpoint = publishEndpoint;
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

            await _publishEndpoint.Publish(new SubscriptionUpgradedEvent(
             subscription.Id,                     
             subscription.UserId,                 
             subscription.Tier,                  
             subscription.ExpiresAt!.Value,     
             DateTime.UtcNow), ct);

            await _db.SaveChangesAsync(ct);

            return Result<UpgradeSubscriptionResponse>.Success(
                new UpgradeSubscriptionResponse(subscription.Tier.ToString(), subscription.ExpiresAt!.Value));
        }
    }
}
