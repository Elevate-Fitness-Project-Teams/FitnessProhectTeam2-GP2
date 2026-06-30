using Elevate.Profile.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Profile.Infrastructure.Persistance.Configrations
{
    public class UserProfilesConfigrations : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(up=>up.UserId);
            builder.Property(up => up.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(up => up.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(up => up.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(up => up.Email)
                .IsRequired()
                .HasMaxLength(100);
  

        }
    }
}
