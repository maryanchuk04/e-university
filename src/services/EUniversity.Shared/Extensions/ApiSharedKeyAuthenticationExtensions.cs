using EUniversity.Shared.Constants;
using EUniversity.Shared.Handlers;
using EUniversity.Shared.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace EUniversity.Shared.Extensions;

public static class ApiSharedKeyAuthenticationExtensions
{
    public const string AuthenticationSchemeName = SharedApiKeyContants.SchemeName;
    private const string AuthenticationSchemeDisplayName = SharedApiKeyContants.SchemeDisplayName;

    /// <summary>
    /// Add Pre-Shared Key Authorization to an IServiceCollection.
    /// </summary>
    public static IServiceCollection AddPreSharedKeyAuthorization(this IServiceCollection services, string preSharedKeyValue)
    {
        Console.WriteLine($"------------------------ API KEY = {preSharedKeyValue} ------------------------");
        services
            .AddAuthentication(options => options.AddSharedKeyScheme())
            .AddSharedKeyAuthentication(x => x.PreSharedKeyValue = preSharedKeyValue);

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
