namespace EUniversity.Shared.Authentication.Models;

/// <summary>
/// General JWT token Claims.
/// </summary>
public static class EUniversityClaimTypes
{
    private const string BaseUrl = "https://e-university.ua.com";

    public const string UserId = $"{BaseUrl}/userId";
    public const string Permissions = $"{BaseUrl}/permissions";
    public const string Roles = $"{BaseUrl}/roles";
    public const string Email = $"{BaseUrl}/email";
}
