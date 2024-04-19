using EUniversity.Authorization.Contract.Requests;
using EUniversity.Authorization.Contract.Response;
using EUniversity.Core.Http;
using Microsoft.Extensions.Logging;

namespace EUniversity.Authorization.Client;

public class AuthorizationClient : MicroservicesClientBase<AuthorizationClient>, IAuthorizationClient
{
    private const string AutheticateRoute = "/api/authenticate";

    public AuthorizationClient(
        string endpoint,
        IHttpClientFactory httpClientFactory,
        ILogger<AuthorizationClient> logger,
        TimeSpan? timeout = null)
        : base(endpoint, httpClientFactory, logger, timeout)
    {
    }

    /// <inheritdoc/>
    public Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<AuthenticateRequest, AuthenticateResponse>(AutheticateRoute, request);
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
}
