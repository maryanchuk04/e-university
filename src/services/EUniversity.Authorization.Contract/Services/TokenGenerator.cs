using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EUniversity.Shared.Authentication.Models;
using EUniversity.Shared.Authentication.Settings;
using EUniversity.Shared.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EUniversity.Authorization.Contract.Services;

public interface ITokenGenerator
{
    /// <summary>
    /// Generate access token for user.
    /// </summary>
    /// <param name="userId">UserId.</param>
    /// <param name="email">User email.</param>
    /// <param name="roles">User roles.</param>
    /// <param name="permissions">User Permissions.</param>
    /// <returns>JWT Access token with user information.</returns>
    string GenerateAccessToken(Guid userId, string email, IList<Core.Enums.Role> roles, IList<string> permissions);

    /// <summary>
    /// Generate refresh token.
    /// </summary>
    /// <returns>Refresh token (in base64string format).</returns>
    string GenerateRefreshToken();
}

public class TokenGenerator(IOptions<JwtSettings> jwtSettings) : ITokenGenerator
{
    private readonly JwtSettings _jwtSettings = jwtSettings.ThrowIfNull().Value.ThrowIfNull();

    // <inheritdoc />
    public string GenerateAccessToken(Guid userId, string email, IList<Core.Enums.Role> roles, IList<string> permissions)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

        var claims = new List<Claim>
        {
            new(EUniversityClaimTypes.Email, email),
            new(EUniversityClaimTypes.UserId, userId.ToString())
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(EUniversityClaimTypes.Roles, role.ToString()));
        }

        foreach (var permission in permissions)
        {
            claims.Add(new Claim(EUniversityClaimTypes.Permissions, permission));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Audience = jwtSettings.Value.Audience,
            Issuer = jwtSettings.Value.Issuer,
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    // <inheritdoc />
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }
}
