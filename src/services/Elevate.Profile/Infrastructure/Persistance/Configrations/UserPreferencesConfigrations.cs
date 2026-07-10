using Elevate.Profile.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Elevate.Profile.Infrastructure.Persistance.Configrations
{
    public class UserPreferencesConfigrations : IEntityTypeConfiguration<UserPreferences>
    {
        public void Configure(EntityTypeBuilder<UserPreferences> builder)
        {
            builder.HasKey(up => up.UserId);
            builder.HasOne(up => up.UserProfile)
                    .WithOne(up => up.Preferences)
                    .HasForeignKey<UserPreferences>(up => up.UserId);





            builder.HasKey(x => x.UserId);

            builder.Property(x => x.UserId)
                   .ValueGeneratedNever();

            builder.Property(x => x.Language)
                   .HasConversion<string>()
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(x => x.Theme)
                   .HasConversion<string>()
                   .HasMaxLength(15)
                   .IsRequired();

            builder.Property(x => x.WeightUnit)
                   .HasConversion<string>()
                   .HasMaxLength(5)
                   .IsRequired();

            builder.Property(x => x.HeightUnit)
                   .HasConversion<string>()
                   .HasMaxLength(5)
                   .IsRequired();

            builder.Property(x => x.DistanceUnit)
                   .HasConversion<string>()
                   .HasMaxLength(5)
                   .IsRequired();

         



        }
    }
}
