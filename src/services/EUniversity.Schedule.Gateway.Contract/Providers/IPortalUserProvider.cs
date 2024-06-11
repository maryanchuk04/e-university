using System.IdentityModel.Tokens.Jwt;
using EUniversity.Core.Enums;
using EUniversity.Schedule.Gateway.Contract.Models;
using EUniversity.Shared.Extensions;
using Microsoft.AspNetCore.Http;

namespace EUniversity.Schedule.Gateway.Contract.Providers;

public interface IPortalUserProvider
{
    IPortalUser GetPortalUser();
}

public class PortalUserProvider(IHttpContextAccessor httpContextAccessor) : IPortalUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor.ThrowIfNull();

    public IPortalUser GetPortalUser()
    {
        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HTTP context is not available.");

        var authHeader = httpContext.Request.Headers.Authorization.FirstOrDefault();
        if (authHeader == null || !authHeader.StartsWith("Bearer "))
        {
            throw new InvalidOperationException("No Bearer token found in the request headers.");
        }

        var token = authHeader["Bearer ".Length..];
        var handler = new JwtSecurityTokenHandler();

        var jsonToken = handler.ReadToken(token) as JwtSecurityToken
            ?? throw new InvalidOperationException("Invalid JWT token.");

        var userInfo = new PortalUser
        {
            UserId = Guid.Parse(jsonToken.Claims.FirstOrDefault(c => c.Type == "https://e-university.ua.com/userId")?.Value),
            Email = jsonToken.Claims.FirstOrDefault(c => c.Type == "https://e-university.ua.com/email")?.Value ?? string.Empty,
            Role = ConvertToEnum<Role>(jsonToken.Claims.Where(c => c.Type == "https://e-university.ua.com/roles").Select(c => c.Value).FirstOrDefault()),
            Permissions = jsonToken.Claims
                .Where(c => c.Type == "https://e-university.ua.com/permissions")
                .Select(c => ConvertToEnum<PermissionType>(c.Value))
                .ToList()
        };

        return userInfo;
    }

    private static T ConvertToEnum<T>(string value) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), value);
    }
}
