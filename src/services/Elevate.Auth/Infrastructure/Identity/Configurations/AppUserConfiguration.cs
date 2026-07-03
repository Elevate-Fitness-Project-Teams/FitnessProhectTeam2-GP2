using Elevate.Auth.Domain.Entities;
using Elevate.Auth.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elevate.Auth.Infrastructure.Persistence.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("Users");

        
        builder.OwnsOne(u => u.Name, b =>
        {
            b.Property(n => n.FirstName)
             .HasColumnName("FirstName")
             .HasMaxLength(50)
             .IsRequired();

            b.Property(n => n.LastName)
             .HasColumnName("LastName")
             .HasMaxLength(50)
             .IsRequired();

            b.Ignore(n => n.DisplayName); 
        });

        builder.Property(u => u.RequiresProfileCompletion)
               .HasDefaultValue(true)
               .IsRequired();

        builder.Property(u => u.IsActive)
               .HasDefaultValue(true)
               .IsRequired();

        builder.Property(u => u.IsLockedOut)
               .HasDefaultValue(false)
               .IsRequired();

        builder.Property(u => u.LockedUntil)
               .IsRequired(false);          // null = not locked

        builder.Property(u => u.CreatedAt)
               .IsRequired();

        builder.Property(u => u.UpdatedAt)
               .IsRequired(false);          
        
        builder.HasMany<LoginAttempt>()
               .WithOne()
               .HasForeignKey(la => la.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<RefreshToken>()
               .WithOne()
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<Domain.Entities.OtpCode>()
               .WithOne()
               .HasForeignKey(o => o.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(u => u.DomainEvents);

        
        builder.HasIndex(u => u.IsLockedOut)
               .HasDatabaseName("IX_Users_IsLockedOut")
               .HasFilter("[IsLockedOut] = 1");
    }
}