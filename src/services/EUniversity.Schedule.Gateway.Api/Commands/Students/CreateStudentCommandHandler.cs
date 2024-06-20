using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Commands.Students;

public class CreateStudentCommand : IRequest<Guid>
{
}

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
{
    public Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
