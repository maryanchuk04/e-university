
using EUniversity.Shared.Extensions;

namespace EUniversity.Authorization.Contract.Clients;

public class AuthorizationClient(HttpClient httpClient) : IAuthorizationClient
{
    private const string RegistrationRoute = "/api/registration";
    private readonly HttpClient _httpClient = httpClient.ThrowIfNull();

    /// <inheritdoc/>
    public Task AuthenticateAsync(string email, CancellationToken cancellationToken = default)
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
        return _httpClient.PostAsync(RegistrationRoute, null,cancellationToken: cancellationToken);
    }
}
