using EUniversity.Authorization.Contract.Requests;
using EUniversity.Authorization.Contract.Response;

namespace EUniversity.Authorization.Client;

public interface IAuthorizationClient
{
    /// <summary>
    /// Authenticate user.
    /// </summary>
    /// <param name="request">Auth request with information about user.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request, CancellationToken cancellationToken = default);

    Task<bool> CheckIfUserExistAsync(string email, CancellationToken cancellationToken = default);

    Task GetUserRoleAsync(string email, CancellationToken cancellationToken = default);

    Task GetUserAsync(string email, CancellationToken cancellationToken = default);
}
