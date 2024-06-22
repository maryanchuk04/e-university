using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Schedule.Manager.Api.Queries.Teachers;

public class GetTeachersByFacultyQuery(Guid facultyId) : IRequest<List<TeacherDto>>
{
    public Guid FacultyId { get; set; } = facultyId;
}

public class GetTeacherByFacultyQueryHandler(UniversityScheduleManagerContext db) : IRequestHandler<GetTeachersByFacultyQuery, List<TeacherDto>>
{
    public async Task<List<TeacherDto>> Handle(GetTeachersByFacultyQuery request, CancellationToken cancellationToken)
    {
        var teachers = await db.Teachers
            .Include(t => t.TeacherFaculties)
            .ThenInclude(t => t.Faculty)
            .ThenInclude(f => f.Dean)
            .Where(t => t.TeacherFaculties.Any(t => t.FacultyId == request.FacultyId))
            .Select(t => new TeacherDto
            {
                Id = t.Id,
                FullName = t.FullName,
                Position = t.Position,
                UserId = t.UserId,
                Faculties = t.TeacherFaculties.Select(x => new FacultyDto
                {
                    Address = x.Faculty.Adress,
                    Dean = x.Faculty.Dean != null ? new TeacherDto(x.Faculty.Dean.Id, x.Faculty.Dean.FullName, x.Faculty.Dean.Position) : null,
                    Name = x.Faculty.Name,
                    Id = x.Id,
                    Description = x.Faculty.Description,
                }).ToList()
            }).ToListAsync(cancellationToken);

        return teachers;
    }
}
