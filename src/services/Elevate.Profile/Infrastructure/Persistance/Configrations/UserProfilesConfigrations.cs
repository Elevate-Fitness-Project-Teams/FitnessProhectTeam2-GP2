using Elevate.Profile.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Profile.Infrastructure.Persistance.Configrations
{
    public class UserProfilesConfigrations : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(up=>up.UserId);


            builder.Property(x => x.UserId)
       .ValueGeneratedNever();

            builder.Property(up => up.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            //builder.Property(up => up.Email)
            //    .IsRequired()
            //    .HasMaxLength(100);

            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(e => e.Value)
                     .HasColumnName("Email")
                     .HasMaxLength(255)
                     .IsRequired();
            });

            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(n => n.FirstName)
                    .HasColumnName("FirstName")
                    .HasMaxLength(50)
                    .IsRequired();
                name.Property(n => n.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(50)
                    .IsRequired();
            });


            builder.HasOne(x => x.Preferences)
       .WithOne(x => x.UserProfile)
       .HasForeignKey<UserPreferences>(x => x.UserId);

            builder.HasOne(x => x.NotificationSettings)
                   .WithOne(x => x.UserProfile)
                   .HasForeignKey<NotificationSettings>(x => x.UserId);

            builder.HasOne(x => x.PrivacySettings)
                   .WithOne(x => x.UserProfile)
                   .HasForeignKey<PrivacySettings>(x => x.UserId);
        }
    }
}
