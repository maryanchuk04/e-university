using Azure;
using EUniversity.Schedule.Gateway.Contract.Responses;

namespace EUniversity.Schedule.Gateway.Api.Extensions;

public static class HttpContextExtensions
{
    private const string AccessTokenKey = "e_access_token";
    private const string RefreshTokenKey = "e_refresh_token";

    public static void SetAuthCookies(this HttpContext context, AuthenticateResponse response, string domain)
    {
        var accessTokenCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None, // Set to None for cross-site cookies
            Domain = domain,
            Expires = DateTime.UtcNow.AddDays(1),
        };
        var refreshTokenCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None, // Set to None for cross-site cookies
            Domain = domain,
            Expires = DateTime.UtcNow.AddDays(15)
        };

        context.Response.Cookies.Append(AccessTokenKey, response.AccessToken, accessTokenCookieOptions);
        context.Response.Cookies.Append(RefreshTokenKey, response.RefreshToken, refreshTokenCookieOptions);
    }
}
