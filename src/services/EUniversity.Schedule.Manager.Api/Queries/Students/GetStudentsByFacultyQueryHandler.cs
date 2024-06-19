using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Schedule.Manager.Api.Queries.Students;

public class GetStudentsByFacultyQuery(Guid facultyId) : IRequest<List<StudentInfoDto>>
{
    public Guid FacultyId { get; set; } = facultyId;
}

public class GetStudentsByFacultyQueryHandler(UniversityScheduleManagerContext db) : IRequestHandler<GetStudentsByFacultyQuery, List<StudentInfoDto>>
{
    public async Task<List<StudentInfoDto>> Handle(GetStudentsByFacultyQuery request, CancellationToken cancellationToken)
    {
        var students = await db.Students
            .Include(s => s.Group)
                .ThenInclude(g => g.Faculty)
            .Include(s => s.Group)
                .ThenInclude(g => g.Speciality)
            .Where(s => s.Group.FacultyId == request.FacultyId)
            .Select(student => new StudentInfoDto
            {
                Id = student.Id,
                UserId = student.UserId,
                GroupId = student.Group.Id,
                GroupName = student.Group.Name,
                FacultyId = student.Group.Faculty.Id,
                FacultyName = student.Group.Faculty.Name,
                SpecialityId = student.Group.Speciality.Id,
                SpecialityName = student.Group.Speciality.Name,
            })
            .ToListAsync(cancellationToken);

        return students;
    }
}
