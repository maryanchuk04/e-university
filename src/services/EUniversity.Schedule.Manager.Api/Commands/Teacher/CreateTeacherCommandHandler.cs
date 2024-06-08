using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Schedule.Manager.Data;
using EUniversity.Shared.Extensions;
using MediatR;

namespace EUniversity.Schedule.Manager.Api.Commands.Teacher;

public class CreateTeacherCommand(CreateTeacherRequest request) : IRequest<Guid>
{
    public CreateTeacherRequest Teacher { get; } = request.ThrowIfNull();
}

public class CreateTeacherCommandHandler(UniversityScheduleManagerContext db) : IRequestHandler<CreateTeacherCommand, Guid>
{
    private readonly UniversityScheduleManagerContext _db = db.ThrowIfNull();

    public async Task<Guid> Handle(CreateTeacherCommand command, CancellationToken cancellationToken)
    {
        var teacher = new Data.Models.Teacher
        {
            Id = Guid.NewGuid(),
            FullName = command.Teacher.FullName,
            Position = command.Teacher.Position,
            UserId = command.Teacher.UserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        if (command.Teacher.FacultyIds.Count > 0)
        {
            foreach (var facultyId in command.Teacher.FacultyIds)
            {
                teacher.TeacherFaculties.Add(new Data.Models.TeacherFaculty
                {
                    FacultyId = facultyId,
                    TeacherId = teacher.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }
        }

        await _db.Teachers.AddAsync(teacher, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return teacher.Id;
    }
}