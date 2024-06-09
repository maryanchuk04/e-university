using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Data;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FacultyEntity = EUniversity.Schedule.Manager.Data.Models.Faculty;

namespace EUniversity.Schedule.Manager.Api.Queries.Faculty;

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
                .ThenInclude(tt => tt!.LessonTimes)
            .ToListAsync(cancellationToken);

        return Map(faculties);
    }

    private static List<FacultyDto> Map(List<FacultyEntity> faculties)
    {
        return faculties.Select(f =>
        {
            var facultyDto = new FacultyDto(f.Id, f.Name, f.Description, f.Adress);

            if (f.TimeTable != null)
            {
                var lessonTimes = f.TimeTable.LessonTimes?
                    .Select(l => new LessonTimeDto(l.Id, l.LessonNumber, l.StartAt, l.EndAt))
                    .ToList() ?? [];

                facultyDto.TimeTable = new TimeTableDto { Id = f.TimeTable.Id, LessonTimes = lessonTimes };
            }

            if (f.Dean != null)
            {
                facultyDto.Dean = new TeacherDto(f.Dean.Id, f.Dean.FullName, f.Dean.Position);
            }

            return facultyDto;
        }).ToList();
    }
}
