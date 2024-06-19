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
    Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request, CancellationToken cancellationToken = default);

    Task<AuthenticateResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

    Task<List<UserResponse>> GetUsersAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Get user by email.
    /// </summary>
    Task<UserResponse> GetUserAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user by id.
    /// </summary>
    Task<UserResponse> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
