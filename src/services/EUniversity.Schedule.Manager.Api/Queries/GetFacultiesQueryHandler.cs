using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Data;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Schedule.Manager.Api.Queries;

public class GetFacultiesQuery : IRequest<IList<FacultyDto>> { }

public class GetFacultiesQueryHandler(UniversityScheduleManagerContext db) : IRequestHandler<GetFacultiesQuery, IList<FacultyDto>>
{
    private readonly UniversityScheduleManagerContext _db = db.ThrowIfNull();

    public async Task<IList<FacultyDto>> Handle(GetFacultiesQuery request, CancellationToken cancellationToken)
    {
        var faculties = await _db.Faculties
                 .AsSplitQuery()
                 .AsNoTracking()
                 .Include(f => f.Dean)
                 .Include(f => f.TimeTable)
                     .ThenInclude(tt => tt.LessonTimes)
                 .Select(f => new FacultyDto
                 {
                     Id = f.Id,
                     Name = f.Name,
                     Description = f.Description,
                     Address = f.Adress,
                     Dean = new TeacherDto
                     {
                         Id = f.Dean.Id,
                         FullName = f.Dean.FullName,
                         Position = f.Dean.Position
                     },
                     TimeTable = new TimeTableDto
                     {
                         Id = f.TimeTable.Id,
                         LessonTimes = f.TimeTable.LessonTimes.Select(lt => new LessonTimeDto
                         {
                             Id = lt.Id,
                             LessonNumber = lt.LessonNumber,
                             StartAt = lt.StartAt,
                             EndAt = lt.EndAt
                         }).ToList()
                     },
                 })
                 .ToListAsync(cancellationToken);

        return faculties;
    }
}
