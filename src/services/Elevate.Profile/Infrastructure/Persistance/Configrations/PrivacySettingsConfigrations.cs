using Elevate.Profile.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Profile.Infrastructure.Persistance.Configrations
{
    public class PrivacySettingsConfigrations : IEntityTypeConfiguration<PrivacySettings>
    {
        public void Configure(EntityTypeBuilder<PrivacySettings> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.ProfileVisibility)
                   .HasConversion<string>();

            builder.HasOne(x =>x.UserProfile)
             .WithOne(x => x.PrivacySettings)
             .HasForeignKey<PrivacySettings>(x => x.UserId);
        }
    }
}
