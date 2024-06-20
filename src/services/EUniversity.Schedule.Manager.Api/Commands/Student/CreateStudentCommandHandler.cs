using EUniversity.Schedule.Manager.Data;
using MediatR;

namespace EUniversity.Schedule.Manager.Api.Commands.Student;

public class CreateStudentCommand : IRequest<Guid>
{
}

public class CreateStudentCommandHandler(UniversityScheduleManagerContext db) : IRequestHandler<CreateStudentCommand, Guid>
{
    public Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
