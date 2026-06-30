using Elevate.Profile.Domain;
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
                    .WithOne(up => up.UserPreferences)
                    .HasForeignKey<UserPreferences>(up => up.UserId);



        }
    }
}
