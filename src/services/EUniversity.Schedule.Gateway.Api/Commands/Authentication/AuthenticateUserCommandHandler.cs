using EUniversity.Authorization.Client;
using EUniversity.Schedule.Gateway.Contract.Requests;
using EUniversity.Schedule.Gateway.Contract.Responses;
using EUniversity.Shared.Extensions;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Commands.Authentication;

public class AuthenticateUserCommand(AuthenticateRequest request) : IRequest<AuthenticateResponse>
{
    public AuthenticateRequest request = request.ThrowIfNull();
}

public class AuthenticateUserCommandHandler(
    IAuthorizationClient authorizationClient,
    ILogger<AuthenticateUserCommandHandler> logger)
    : IRequestHandler<AuthenticateUserCommand, AuthenticateResponse>
{
    private readonly ILogger<AuthenticateUserCommandHandler> _logger = logger.ThrowIfNull();
    private readonly IAuthorizationClient _authorizationClient = authorizationClient.ThrowIfNull();

    public async Task<AuthenticateResponse> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.ThrowIfNull().request.ThrowIfNull();

        try
        {
            var authResponse = await _authorizationClient.AuthenticateAsync(
                new Authorization.Contract.Requests.AuthenticateRequest(request.Email, request.Picture), cancellationToken);

            return new AuthenticateResponse(authResponse.AccessToken, authResponse.RefreshToken, authResponse.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unnable to authenticate user with email = '{Email}'", request.Email);
            throw;
        }
    }
}

