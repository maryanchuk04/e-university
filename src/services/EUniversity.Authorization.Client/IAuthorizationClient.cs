namespace EUniversity.Authorization.Client;

public interface IAuthorizationClient
{
    /// <summary>
    /// Registers a new user in the system and defines a role.
    /// </summary>
    /// <param name="email">User email.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    Task RegisterUserAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Authenticate user.
    /// </summary>
    /// <param name="email">User email.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    Task AuthenticateAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> CheckIfUserExistAsync(string email, CancellationToken cancellationToken = default);

    Task GetUserRoleAsync(string email, CancellationToken cancellationToken = default);

    Task GetUserAsync(string email, CancellationToken cancellationToken = default);
}
