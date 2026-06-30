using Elevate.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(rt => rt.Id);
        builder.Property(rt => rt.Id).ValueGeneratedNever();

        builder.Property(rt => rt.UserId).IsRequired();

        // SHA-256 hash = 64 hex chars; allow some headroom
        builder.Property(rt => rt.TokenHash)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(rt => rt.ExpiresAt).IsRequired();
        builder.Property(rt => rt.CreatedAt).IsRequired();
        builder.Property(rt => rt.RevokedAt).IsRequired(false);

        // ── Indexes ───────────────────────────────────────────────────────────

        // Lookup by hash on every request (validate + rotate)
        builder.HasIndex(rt => rt.TokenHash)
            .IsUnique()
            .HasDatabaseName("IX_RefreshTokens_TokenHash");

        // Revoke-all query: WHERE UserId = ? AND RevokedAt IS NULL
        builder.HasIndex(rt => new { rt.UserId, rt.RevokedAt })
            .HasDatabaseName("IX_RefreshTokens_UserId_RevokedAt");
    }
}
