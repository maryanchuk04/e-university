using EUniversity.Authorization.Contract.Clients;
using EUniversity.Gateway.Contract.Requests;
using EUniversity.Shared.Extensions;
using MediatR;

namespace EUniversity.Gateway.Api.Commands;

public class AuthenticateUserCommand(AuthenticateRequest request) : IRequest<string>
{
    public AuthenticateRequest request = request.ThrowIfNull();
}

public class AuthenticateUserCommandHandler(IAuthorizationClient authorizationClient) : IRequestHandler<AuthenticateUserCommand, string>
{
    private readonly IAuthorizationClient _authorizationClient = authorizationClient.ThrowIfNull();

    public Task<string> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        command.ThrowIfNull().request.ThrowIfNull();

        //await _authorizationClient.AuthenticateAsync();
        return Task.FromResult("");
    }
}

