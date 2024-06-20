using EUniversity.Schedule.Manager.Contract.Exceptions;
using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Data;
using EUniversity.Schedule.Manager.Data.Models;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Schedule.Manager.Api.Queries.Faculty;

public class GetFacultyTimetableQuery(Guid facultyId) : IRequest<TimeTableDto>
{
    public Guid FacultyId { get; set; } = facultyId;
}

public class GetFacultyTimetableQueryHandler(UniversityScheduleManagerContext db) : IRequestHandler<GetFacultyTimetableQuery, TimeTableDto>
{
    private readonly UniversityScheduleManagerContext _db = db.ThrowIfNull();

    public async Task<TimeTableDto> Handle(GetFacultyTimetableQuery query, CancellationToken cancellationToken)
    {
        var timetable = await _db.TimeTables
            .AsNoTracking()
            .AsSplitQuery()
            .Include(tt => tt.LessonTimes)
            .Include(tt => tt.Faculty)
            .FirstOrDefaultAsync(tt => tt.FacultyId == query.FacultyId, cancellationToken: cancellationToken)
        ?? throw new EntityNotFoundException(nameof(TimeTable), nameof(query.FacultyId), query.FacultyId.ToString());

        return new TimeTableDto(
            timetable.Id,
            timetable.LessonTimes.Select(lt => new LessonTimeDto(lt.Id, lt.LessonNumber, lt.StartAt, lt.EndAt)).ToList(),
            timetable.Faculty.Name);
    }
}
