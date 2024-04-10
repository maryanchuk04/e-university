using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EUniversity.Authorization.Data.Models;
using EUniversity.Shared.Auth.Settings;
using EUniversity.Shared.Authentication.Models;
using EUniversity.Shared.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EUniversity.Authorization.Contract.Services;

public interface ITokenGenerator
{
    string GenerateAccessToken(Guid userId, string email, IList<Core.Enums.Role> roles);

    string GenerateRefreshToken(UserRole userRole);
}

public class TokenGenerator(IOptions<JwtSettings> jwtSettings) : ITokenGenerator
{
    private readonly JwtSettings _jwtSettings = jwtSettings.ThrowIfNull().Value.ThrowIfNull();

    public string GenerateAccessToken(Guid userId, string email, IList<Core.Enums.Role> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Email, email),
            new(EUniversityClaimTypes.Email, email),
            new(EUniversityClaimTypes.UserId, userId.ToString())
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(EUniversityClaimTypes.Permissions, role.ToString()));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),

            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken(UserRole userRole)
    {
        throw new NotImplementedException();
    }
}
