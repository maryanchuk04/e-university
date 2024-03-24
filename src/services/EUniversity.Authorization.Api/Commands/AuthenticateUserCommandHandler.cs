using EUniversity.Authorization.Api.Queries;
using EUniversity.Authorization.Contract.Requests;
using EUniversity.Shared.Extensions;
using MediatR;

namespace EUniversity.Authorization.Api.Commands;

public class AuthenticateUserCommand(AuthenticateRequest request) : IRequest
{
    public AuthenticateRequest Request { get; set; } = request.ThrowIfNull();
}

public class AuthenticateUserCommandHandler(IMediator mediator) : IRequestHandler<AuthenticateUserCommand>
{
    private readonly IMediator _mediator = mediator.ThrowIfNull();

    public async Task Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        if (await _mediator.Send(new CheckIfUserExistQuery(command.Request.Email), cancellationToken))
        {

        }
    }
}
