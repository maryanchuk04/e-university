using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Schedule.Manager.Data;
using StudentEntity = EUniversity.Schedule.Manager.Data.Models.Student;
using MediatR;

namespace EUniversity.Schedule.Manager.Api.Commands.Student;

public class CreateStudentCommand(CreateStudentRequest student) : IRequest<Guid>
{
    public CreateStudentRequest Student { get; set; } = student;
}

public class CreateStudentCommandHandler(UniversityScheduleManagerContext db) : IRequestHandler<CreateStudentCommand, Guid>
{
    public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var studentReq =  request.Student;

        var id = Guid.NewGuid();

        var student = new StudentEntity
        { 
            UserId = studentReq.UserId,
            GroupId = studentReq.GroupId,
            Id = id,
        };

        await db.Students.AddAsync(student, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        return id;
    }
}
