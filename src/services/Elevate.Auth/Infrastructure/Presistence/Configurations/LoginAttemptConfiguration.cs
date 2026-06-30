using Elevate.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.Persistence.Configurations;

public class LoginAttemptConfiguration : IEntityTypeConfiguration<LoginAttempt>
{
    public void Configure(EntityTypeBuilder<LoginAttempt> builder)
    {
        builder.ToTable("LoginAttempts");
        builder.HasKey(la => la.Id);
        builder.Property(la => la.Id).ValueGeneratedNever();

        builder.Property(la => la.UserId).IsRequired();
        builder.Property(la => la.IsSuccess).IsRequired();

        builder.Property(la => la.IpAddress)
            .HasMaxLength(45) // covers IPv6
            .IsRequired();

        builder.Property(la => la.AttemptedAt).IsRequired();

        // ── Indexes ───────────────────────────────────────────────────────────
        // Lockout window query:
        // WHERE UserId = ? AND IsSuccess = false AND AttemptedAt >= (now - 15min)
        builder.HasIndex(la => new { la.UserId, la.IsSuccess, la.AttemptedAt })
            .HasDatabaseName("IX_LoginAttempts_UserId_IsSuccess_AttemptedAt");
    }
}
