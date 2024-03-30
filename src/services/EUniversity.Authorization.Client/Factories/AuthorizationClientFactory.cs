using System.Net.Http.Headers;
using EUniversity.Shared.Swagger;

namespace EUniversity.Authorization.Client.Factories;

/// <summary>
/// Factory that`s create instance of IAuthorizationClient.
/// </summary>
public interface IAuthorizationClientFactory
{
    IAuthorizationClient Create(string baseUrl, string apiKey);
}

/// <inheritdoc/>
public class AuthorizationClientFactory : IAuthorizationClientFactory
{
    public IAuthorizationClient Create(string baseAddress, string apiKey)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseAddress)
        };
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(ApiKeyConstants.HeaderName, apiKey);
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        return new AuthorizationClient(httpClient);
    }
}
