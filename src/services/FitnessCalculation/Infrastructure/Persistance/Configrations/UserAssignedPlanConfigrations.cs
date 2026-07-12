using Elevate.FitnessCalculation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elevate.FitnessCalculation.Infrastructure.Persistance.Configrations
{
    public class UserAssignedPlanConfigrations : IEntityTypeConfiguration<UserAssignedPlans>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserAssignedPlans> builder)
        {
            builder.ToTable("UserAssignedPlans");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserId).IsRequired();
            builder.Property(u => u.PlanId).IsRequired();
            builder.Property(u => u.AssignedAt)
                    .HasDefaultValueSql("GETDATE()");
            builder.HasIndex(x => x.UserId);
        }
    }
}
