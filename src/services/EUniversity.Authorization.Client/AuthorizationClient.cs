using System.Net.Http;
using EUniversity.Authorization.Contract.Requests;
using EUniversity.Authorization.Contract.Response;
using EUniversity.Core.Http;
using EUniversity.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace EUniversity.Authorization.Client;

public class AuthorizationClient : MicroservicesClientBase<AuthorizationClient>, IAuthorizationClient
{
    private const string RegistrationRoute = "/api/registration";
    private const string AutheticateRoute = "/api/authenticate";

    public AuthorizationClient(string endpoint,
        IHttpClientFactory httpClientFactory,
        ILogger<AuthorizationClient> logger,
        TimeSpan? timeout = null)
        : base(endpoint, httpClientFactory, logger, timeout)
    {
    }

    /// <inheritdoc/>
    public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request, CancellationToken cancellationToken = default)
    {
        await PostAsync(AutheticateRoute, request, cancellationToken);
    }

    public Task<bool> CheckIfUserExistAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task GetUserAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task GetUserRoleAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task RegisterUserAsync(string email, CancellationToken cancellationToken = default)
    {
        return _httpClient.PostAsync(RegistrationRoute, null, cancellationToken: cancellationToken);
    }
}
