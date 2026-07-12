using Elevate.subscription.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace Elevate.Subscription.Infrastructure.Persistence;

// DbContext — inherits BaseDbContext, scans for + dispatches those events automatically
public class SubscriptionDbContext : BaseDbContext
{
    public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options, IMediator mediator)
        : base(options, mediator)
    {
    }

    public DbSet<UserSubscription> UserSubscriptions => Set<UserSubscription>();
    public DbSet<BillingLog> BillingLogs => Set<BillingLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SubscriptionDbContext).Assembly);
        modelBuilder.AddTransactionalOutboxEntities();
        base.OnModelCreating(modelBuilder);
    }
}

