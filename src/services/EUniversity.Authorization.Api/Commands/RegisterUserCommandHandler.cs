using MediatR;

namespace EUniversity.Authorization.Api.Commands;

public class RegisterUserCommand(string email, string hd) : IRequest
{
    public string Email { get; set; } = email;
    public string Hd { get; set; } = hd;
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    public Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
