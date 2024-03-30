using Microsoft.AspNetCore.Authentication;

namespace BizPlanner.AuthorizationMiddleware.Options;

/// <summary>
/// Options for ApiSharedKeyAuthenticationSchemeOptions
/// </summary>
public class ApiSharedKeyAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    /// <summary>
    /// The value of the secret key.
    /// </summary>
    public string PreSharedKeyValue { get; set; }
}
