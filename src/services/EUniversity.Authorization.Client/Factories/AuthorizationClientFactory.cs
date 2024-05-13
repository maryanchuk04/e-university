using System.Net.Http.Headers;
using EUniversity.Shared.Constants;
using EUniversity.Shared.Extensions;
using EUniversity.Shared.Swagger;
using Microsoft.Extensions.Logging;

namespace EUniversity.Authorization.Client.Factories;

/// <summary>
/// Factory that`s create instance of IAuthorizationClient.
/// </summary>
public interface IAuthorizationClientFactory
{
    IAuthorizationClient Create(string baseUrl, string apiKey);
}

/// <inheritdoc/>
public class AuthorizationClientFactory(IHttpClientFactory httpClientFactory, ILogger<AuthorizationClient> logger) : IAuthorizationClientFactory
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory.ThrowIfNull();
    private readonly ILogger<AuthorizationClient> _logger = logger.ThrowIfNull();

    public IAuthorizationClient Create(string baseAddress, string apiKey)
    {
        return new AuthorizationClient(baseAddress, apiKey, _httpClientFactory, _logger);
    }
}
