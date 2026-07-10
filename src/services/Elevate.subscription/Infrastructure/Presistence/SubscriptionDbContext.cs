using Elevate.subscription.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Elevate.Subscription.Infrastructure.Persistence;

public sealed class SubscriptionDbContext : BaseDbContext
{
    public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options, IMediator mediator) : base(options, mediator)
    {
    }

    public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
    public DbSet<UserSubscription> UserSubscriptions => Set<UserSubscription>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SubscriptionDbContext).Assembly);
    }


}