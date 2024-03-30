using EUniversity.Shared.Handlers;
using EUniversity.Shared.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace EUniversity.Shared.Extensions;

public static class ApiSharedKeyAuthenticationExtensions
{
    public const string AuthenticationSchemeName = "Api Shared Key Scheme";
    private const string AuthenticationSchemeDisplayName = "Api Shared Key Authentication Scheme";

    /// <summary>
    /// Add Pre-Shared Key Authorization to an IServiceCollection.
    /// </summary>
    public static IServiceCollection AddPreSharedKeyAuthorization(this IServiceCollection services, string preSharedKeyValue)
    {
        services
            .AddAuthentication(options => options.AddSharedKeyScheme());
        return services;
    }

    /// <summary>
    /// Add Shared Key Authentication to an AuthenticationBuilder.
    /// </summary>
    public static AuthenticationBuilder AddSharedKeyAuthentication(
        this AuthenticationBuilder builder, Action<ApiSharedKeyAuthenticationSchemeOptions> options)
    {
        return builder.AddScheme<ApiSharedKeyAuthenticationSchemeOptions, ApiSharedKeyAuthenticationHandler>(
            AuthenticationSchemeName, AuthenticationSchemeDisplayName, options);
    }

    /// <summary>
    /// Add the Shared Key Scheme to an AuthenticationOptions.
    /// </summary>
    /// <param name="authenticationOptions"></param>
    public static void AddSharedKeyScheme(this AuthenticationOptions authenticationOptions)
    {
        authenticationOptions.DefaultAuthenticateScheme = AuthenticationSchemeName;
        authenticationOptions.DefaultChallengeScheme = AuthenticationSchemeName;
    }
}
