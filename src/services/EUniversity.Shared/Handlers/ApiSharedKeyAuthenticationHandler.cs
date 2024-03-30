using System.Text.Encodings.Web;
using EUniversity.Shared.Extensions;
using EUniversity.Shared.Options;
using EUniversity.Shared.Swagger;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EUniversity.Shared.Handlers;

public class ApiSharedKeyAuthenticationHandler : AuthenticationHandler<ApiSharedKeyAuthenticationSchemeOptions>
{
    private const string BizApiSharedKeyAuthHeader = ApiKeyConstants.HeaderName;

    public ApiSharedKeyAuthenticationHandler(
        IOptionsMonitor<ApiSharedKeyAuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (Request.Headers.TryGetValue(BizApiSharedKeyAuthHeader, out var providedAuthentaionKey))
        {
            if (providedAuthentaionKey == Options.PreSharedKeyValue)
            {

                return Task.FromResult(AuthenticateResult.Success(
                    new AuthenticationTicket(new System.Security.Claims.ClaimsPrincipal(),
                    ApiSharedKeyAuthenticationExtensions.AuthenticationSchemeName)));
            }
            else
            {
                return Task.FromResult(AuthenticateResult.Fail("Incorrect Authentication Key"));
            }
        }
        else
        {
            return Task.FromResult(AuthenticateResult.Fail("No Authentication Key Provided"));
        }
    }
}
