using System.Text.Encodings.Web;
using EUniversity.Shared.Constants;
using EUniversity.Shared.Extensions;
using EUniversity.Shared.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EUniversity.Shared.Handlers;

public class ApiSharedKeyAuthenticationHandler : AuthenticationHandler<ApiSharedKeyAuthenticationSchemeOptions>
{
    private const string ApiSharedKeyAuthHeader = SharedApiKeyContants.HeaderName;

    public ApiSharedKeyAuthenticationHandler(
        IOptionsMonitor<ApiSharedKeyAuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(ApiSharedKeyAuthHeader, out var providedAuthKey))
        {
            return Task.FromResult(AuthenticateResult.Fail("No authentication key provided"));
        }

        var providedKey = providedAuthKey.ToString();

        if (string.IsNullOrWhiteSpace(providedKey))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid authentication key provided"));
        }

        if (providedKey == Options.PreSharedKeyValue)
        {
            Console.WriteLine($"Authentication successful with key: {providedKey}");
            var identity = new System.Security.Claims.ClaimsIdentity(ApiSharedKeyAuthenticationExtensions.AuthenticationSchemeName);
            var principal = new System.Security.Claims.ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, ApiSharedKeyAuthenticationExtensions.AuthenticationSchemeName);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        else
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid authentication key"));
        }
    }
}
