using EUniversity.Authorization.Contract.Requests;
using EUniversity.Authorization.Contract.Response;
using EUniversity.Authorization.Data.Models;
using EUniversity.Core.Http;
using Microsoft.Extensions.Logging;

namespace EUniversity.Authorization.Client;

public class AuthorizationClient : MicroservicesClientBase<AuthorizationClient>, IAuthorizationClient
{
    private const string AutheticateRoute = "/api/authenticate";
    private const string UserRoute = "/api/user";

    public AuthorizationClient(
        string endpoint,
        string apiKey,
        IHttpClientFactory httpClientFactory,
        ILogger<AuthorizationClient> logger,
        TimeSpan? timeout = null)
        : base(endpoint, apiKey, httpClientFactory, logger,timeout)
    {
    }

    /// <inheritdoc />
    public Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<AuthenticateRequest, AuthenticateResponse>(AutheticateRoute, request, cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public Task<UserResponse> GetUserAsync(string email, CancellationToken cancellationToken = default)
    {
        return GetAsync<UserResponse>($"{UserRoute}?email={email}", cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public Task<UserResponse> GetUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return GetAsync<UserResponse>($"{UserRoute}/{userId}", cancellationToken: cancellationToken);
    }
}
