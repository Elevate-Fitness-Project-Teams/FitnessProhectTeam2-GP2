using Elevate.Auth.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Auth.Infrastructure.Persistence.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("Users");

        builder.OwnsOne(u => u.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.FirstName)
                       .HasColumnName("FirstName")
                       .HasMaxLength(50)
                       .IsRequired();

            nameBuilder.Property(n => n.LastName)
                       .HasColumnName("LastName")
                       .HasMaxLength(50)
                       .IsRequired();
        });

        builder.Property(u => u.RequiresProfileCompletion)
               .HasDefaultValue(true);

        builder.Property(u => u.IsActive)
               .HasDefaultValue(true);
    }
}