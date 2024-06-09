using EUniversity.Schedule.Manager.Contract.Exceptions;
using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Data;
using EUniversity.Schedule.Manager.Data.Models;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Schedule.Manager.Api.Queries.Students;

public class GetStudentInfoQuery(Guid userId) : IRequest<StudentInfoDto>
{
    public Guid UserId { get; set; } = userId;
}

public class GetStudentInfoQueryHandler(UniversityScheduleManagerContext db) : IRequestHandler<GetStudentInfoQuery, StudentInfoDto>
{
    private readonly UniversityScheduleManagerContext _db = db.ThrowIfNull();

    public async Task<StudentInfoDto> Handle(GetStudentInfoQuery request, CancellationToken cancellationToken)
    {
        var student = await _db.Students
            .Include(s => s.Group)
                .ThenInclude(g => g.Faculty)
            .Include(s => s.Group)
                .ThenInclude(g => g.Speciality)
            .FirstOrDefaultAsync(s => s.UserId == request.UserId, cancellationToken)
                ?? throw new EntityNotFoundException(nameof(Student), nameof(request.UserId), request.UserId.ToString());

        var studentDto = new StudentInfoDto
        {
            Id = student.Id,
            UserId = student.UserId,
            GroupId = student.Group.Id,
            GroupName = student.Group.Name,
            FacultyId = student.Group.Faculty.Id,
            FacultyName = student.Group.Faculty.Name,
            SpecialityId = student.Group.Speciality.Id,
            SpecialityName = student.Group.Speciality.Name,
        };

        return studentDto;
    }
}
