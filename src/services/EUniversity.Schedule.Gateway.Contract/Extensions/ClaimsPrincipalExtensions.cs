using System.Security.Claims;
using EUniversity.Core.Enums;

namespace EUniversity.Schedule.Gateway.Contract.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst("https://e-university.ua.com/userId");
        return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : Guid.Empty;
    }

    public static string GetEmail(this ClaimsPrincipal user)
    {
        return user.FindFirst("https://e-university.ua.com/email")?.Value ?? string.Empty;
    }

    public static Role GetRoles(this ClaimsPrincipal user)
    {
        var roles = user.FindAll("https://e-university.ua.com/roles").Select(c => c.Value);

        return roles
            .Select(role => Enum.TryParse<Role>(role, out var parsedRole) ? parsedRole : Role.User)
            .Aggregate((current, next) => current | next);
    }

    public static PermissionType GetPermissions(this ClaimsPrincipal user)
    {
        var permissions = user.FindAll("https://e-university.ua.com/permissions").Select(c => c.Value);

        return permissions
            .Select(permission => Enum.TryParse<PermissionType>(permission, out var parsedPermission) ? parsedPermission : PermissionType.NoAccess)
            .Aggregate((current, next) => current | next);
    }
}