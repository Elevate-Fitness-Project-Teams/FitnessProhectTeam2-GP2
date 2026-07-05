using Elevate.Auth.Domain.Entities;
using Elevate.Auth.Infrastructure.Presistence.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Elevate.Auth.Infrastructure.Identity;

public sealed class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IPasswordHasher<AppUser> _passwordHasher;

    public TokenService(IConfiguration configuration, IPasswordHasher<AppUser> passwordHasher)
    {
        _configuration = configuration;
        _passwordHasher = passwordHasher;
    }

    public (string Jwt, string RawRefreshToken, string RefreshTokenHash, DateTime ExpiresAt) GenerateTokenPair(Guid userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"] ?? "ElevateSuperSecretKey1235689!");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(15), 
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["JwtSettings:Issuer"],
            Audience = _configuration["JwtSettings:Audience"]
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(securityToken);

        var rawRefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var refreshTokenHash = HashRefreshToken(rawRefreshToken);

        var expiresAt = DateTime.UtcNow.AddDays(7);

        return (jwt, rawRefreshToken, refreshTokenHash, expiresAt);
    }

    public (string RawToken, string TokenHash) GenerateResetToken()
    {
        var rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        var tokenHash = HashResetToken(rawToken);

        return (rawToken, tokenHash);
    }

    public string HashRefreshToken(string rawToken)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawToken));
        return Convert.ToBase64String(bytes);
    }

    public string HashResetToken(string rawToken)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawToken));
        return Convert.ToBase64String(bytes);
    }
}