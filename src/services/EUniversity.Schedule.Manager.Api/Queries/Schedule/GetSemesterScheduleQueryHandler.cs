using EUniversity.Schedule.Manager.Contract.Exceptions;
using EUniversity.Schedule.Manager.Contract.Models.SemesterSchedule;
using EUniversity.Schedule.Manager.Contract.Responses;
using EUniversity.Schedule.Manager.Data;
using EUniversity.Schedule.Manager.Data.Models;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Schedule.Manager.Api.Queries.Schedule;

public class GetSemesterScheduleQuery(Guid facultyId) : IRequest<ScheduleResponse>
{
    public Guid FacultyId { get; } = facultyId;
}

public class GetSemesterScheduleQueryHandler(UniversityScheduleManagerContext db) : IRequestHandler<GetSemesterScheduleQuery, ScheduleResponse>
{
    private readonly UniversityScheduleManagerContext _db = db.ThrowIfNull();

    public async Task<ScheduleResponse> Handle(GetSemesterScheduleQuery request, CancellationToken cancellationToken)
    {
        // Get the semester
        var semester = await _db.Semesters
            .AsNoTracking()
            .Include(s => s.Schedule)
            // Get last created.
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync(s => s.FacultyId == request.FacultyId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Semester), nameof(request.FacultyId), request.FacultyId.ToString());

        // Get weeks
        var evenWeek = await GetWeekDetailsAsync(semester.Schedule.EvenWeekId, cancellationToken);
        var oddWeek = await GetWeekDetailsAsync(semester.Schedule.OddWeekId, cancellationToken);

        // Map to response
        return new ScheduleResponse
        {
            SemesterId = semester.Id,
            StartDate = semester.StartDate,
            EndDate = semester.EndDate,
            EvenWeek = MapToWeekDto(evenWeek),
            OddWeek = MapToWeekDto(oddWeek)
        };
    }

    private async Task<Week> GetWeekDetailsAsync(Guid weekId, CancellationToken cancellationToken)
    {
        return await _db.Weeks
            .AsSplitQuery()
            .AsNoTracking()
            .Include(w => w.Days)
                .ThenInclude(d => d.Lessons)
                    .ThenInclude(l => l.Room)
            .Include(w => w.Days)
                .ThenInclude(d => d.Lessons)
                    .ThenInclude(l => l.Teacher)
            .Include(w => w.Days)
                .ThenInclude(d => d.Lessons)
                    .ThenInclude(l => l.Subject)
            .Include(w => w.Days)
                .ThenInclude(d => d.Lessons)
                    .ThenInclude(l => l.LessonTime)
            .FirstOrDefaultAsync(w => w.Id == weekId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Week), nameof(Week.Id), weekId.ToString());
    }

    private static WeekDto MapToWeekDto(Week week)
    {
        return new WeekDto
        {
            WeekId = week.Id,
            GroupsSchedule = week.Days
                .GroupBy(d => d.Lessons.First().GroupId)
                .Select(g => new GroupScheduleDto
                {
                    GroupId = g.Key,
                    Days = [.. g.Select(d => new DayScheduleDto
                    {
                        Day = d.DayOfWeek,
                        Lessons = [.. d.Lessons.Select(l => new LessonDto
                        {
                            Id = l.Id,
                            LessonNumber = l.LessonNumber,
                            LessonName = l.Subject?.Name,
                            TeacherId = l.TeacherId,
                            TeacherName = l.Teacher?.FullName,
                            RoomId = l.RoomId ?? Guid.Empty,
                            RoomName = l.Room?.Name,
                            Type = l.Type,
                            Url = l.Url,
                            IsOnline = l.IsOnline,
                            StartAt = l.LessonTime.StartAt,
                            EndAt = l.LessonTime.EndAt,
                        }).OrderBy(l => l.LessonNumber)]
                    }).OrderBy(d => d.Day)]
                }).ToList()
        };
    }
}