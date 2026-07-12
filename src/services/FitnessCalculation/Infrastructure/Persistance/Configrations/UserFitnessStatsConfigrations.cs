using Elevate.FitnessCalculation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.FitnessCalculation.Infrastructure.Persistance.Configrations
{
    public class UserFitnessStatsConfiguration : IEntityTypeConfiguration<UserFitnessStats>
    {
        public void Configure(EntityTypeBuilder<UserFitnessStats> builder)
        {
            builder.ToTable("UserFitnessStats");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                   .IsRequired();

            builder.HasIndex(x => x.UserId).IsUnique();

            builder.Property(x => x.Gender)
                   .HasConversion<string>()
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(x => x.Goal)
                   .HasConversion<string>()
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.ActivityLevel)
                   .HasConversion<string>()
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(x => x.RecordedAt)
                   .IsRequired();

            builder.OwnsOne(x => x.BodyMetrics, body =>
            {
                body.Property(x => x.Weight)
                    .HasColumnName("Weight")
                    .IsRequired();

                body.Property(x => x.Height)
                    .HasColumnName("Height")
                    .IsRequired();

                body.Property(x => x.Age)
                    .HasColumnName("Age")
                    .IsRequired();
            });
            builder.HasOne(x => x.CalculatedMetrics)
                   .WithOne(x => x.UserFitnessStats)
                   .HasForeignKey<CalculatedMetrics>(x => x.UserId)
                   .HasPrincipalKey<UserFitnessStats>(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}