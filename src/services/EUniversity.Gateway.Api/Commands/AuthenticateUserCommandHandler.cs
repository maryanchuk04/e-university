using EUniversity.Authorization.Client;
using EUniversity.Gateway.Contract.Models;
using EUniversity.Gateway.Contract.Requests;
using EUniversity.Shared.Extensions;
using MediatR;

namespace EUniversity.Gateway.Api.Commands;

public class AuthenticateUserCommand(AuthenticateRequest request) : IRequest<TokenDTO>
{
    public AuthenticateRequest request = request.ThrowIfNull();
}

public class AuthenticateUserCommandHandler(IAuthorizationClient authorizationClient) : IRequestHandler<AuthenticateUserCommand, TokenDTO>
{
    private readonly IAuthorizationClient _authorizationClient = authorizationClient.ThrowIfNull();

    public async Task<TokenDTO> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.ThrowIfNull().request.ThrowIfNull();

        if (!request.IsEmailVerified)
            throw new Exception($"Email = {request.Email} in not verified!");
        await _authorizationClient.AuthenticateAsync(
            new Authorization.Contract.Requests.AuthenticateRequest(request.Email, request.Picture), cancellationToken);
    }
}

