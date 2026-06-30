using Elevate.Profile.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Profile.Infrastructure.Persistance.Configrations
{
    public class NotificationSettingsConfigrations : IEntityTypeConfiguration<NotificationSettings>
    {
        public void Configure(EntityTypeBuilder<NotificationSettings> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.HasOne(x => x.UserProfile)
                    .WithOne(x => x.NotificationSettings)
                    .HasForeignKey<UserProfile>(x => x.UserId);
        }
    }
}
