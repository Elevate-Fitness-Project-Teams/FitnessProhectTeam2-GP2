using Elevate.subscription.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.subscription.Infrastructure.Presistence.Configurations
{
    public class BillingLogConfiguration : IEntityTypeConfiguration<BillingLog>
    {
        public void Configure(EntityTypeBuilder<BillingLog> builder)
        {
            builder.ToTable("BillingLogs");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.UserId)
                .IsRequired();

            builder.HasIndex(b => b.UserId);

           
            builder.Property(b => b.SubscriptionId);

            builder.Property(b => b.PlanTier)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(b => b.DurationMonths)
                .IsRequired();

            builder.Property(b => b.PaymentStatus)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(b => b.FailureReason)
                .HasMaxLength(500);

            builder.Property(b => b.ProcessedAt)
                .IsRequired();

            
        }
    }
}
