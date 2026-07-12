using Elevate.FitnessCalculation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.FitnessCalculation.Infrastructure.Persistance.Configrations
{
    public class UserPlanHistoryConfigrations : IEntityTypeConfiguration<UserPlanHistory>
    {
        public void Configure(EntityTypeBuilder<UserPlanHistory> builder)
        {
            builder.ToTable("UserPlanHistories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.PlanId).IsRequired();
            builder.HasIndex(x => x.UserId);
        }
    }
}
