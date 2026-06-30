using Elevate.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.Persistence.Configurations;

public class OtpCodeConfiguration : IEntityTypeConfiguration<OtpCode>
{
    public void Configure(EntityTypeBuilder<OtpCode> builder)
    {
        builder.ToTable("OtpCodes");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedNever();

        builder.Property(o => o.UserId).IsRequired();

        builder.Property(o => o.CodeHash)
            .HasMaxLength(512)
            .IsRequired();

        // Nullable — only set after OTP verified (Spec 1.5)
        builder.Property(o => o.ResetTokenHash)
            .HasMaxLength(512)
            .IsRequired(false);

        builder.Property(o => o.ExpiresAt).IsRequired();
        builder.Property(o => o.CreatedAt).IsRequired();

        builder.Property(o => o.IsUsed)
            .IsRequired()
            .HasDefaultValue(false);

        // ── Indexes ───────────────────────────────────────────────────────────

        // Resend cooldown check: get latest OTP per user
        builder.HasIndex(o => new { o.UserId, o.CreatedAt })
            .HasDatabaseName("IX_OtpCodes_UserId_CreatedAt");

        // Reset token lookup during reset-password (Spec 1.6)
        builder.HasIndex(o => o.ResetTokenHash)
            .HasDatabaseName("IX_OtpCodes_ResetTokenHash")
            .HasFilter("[ResetTokenHash] IS NOT NULL"); // partial index — SQL Server syntax
    }
}
