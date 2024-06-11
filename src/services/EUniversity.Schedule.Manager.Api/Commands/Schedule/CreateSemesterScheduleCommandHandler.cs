using EUniversity.Core.Enums;
using EUniversity.Schedule.Manager.Contract.Exceptions;
using EUniversity.Schedule.Manager.Contract.Models.SemesterSchedule;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Schedule.Manager.Data;
using EUniversity.Schedule.Manager.Data.Models;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScheduleEntity = EUniversity.Schedule.Manager.Data.Models.Schedule;

namespace EUniversity.Schedule.Manager.Api.Commands.Schedule;

public class CreateSemesterScheduleCommand(CreateSemesterScheduleForFacultyRequest request) : IRequest<Unit>
{
    public CreateSemesterScheduleForFacultyRequest Request { get; } = request.ThrowIfNull();
}

public class CreateSemesterScheduleCommandHandler(UniversityScheduleManagerContext db) : IRequestHandler<CreateSemesterScheduleCommand, Unit>
{
    private readonly UniversityScheduleManagerContext _db = db.ThrowIfNull();

    public async Task<Unit> Handle(CreateSemesterScheduleCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var faculty = await _db.Faculties
            .Include(f => f.TimeTable)
                .ThenInclude(tt => tt.LessonTimes)
            .FirstOrDefaultAsync(x => x.Id == request.FacultyId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Faculty), nameof(request.FacultyId), request.FacultyId.ToString());

        // Create weeks
        var evenWeek = new Week { Id = Guid.NewGuid(), Type = WeekType.Even };
        var oddWeek = new Week { Id = Guid.NewGuid(), Type = WeekType.Odd };

        // Create the semester
        var semester = new Semester
        {
            Id = Guid.NewGuid(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            FacultyId = faculty.Id,
            Schedule = new ScheduleEntity
            {
                Id = Guid.NewGuid(),
                EvenWeekId = evenWeek.Id,
                OddWeekId = oddWeek.Id
            }
        };

        _db.Weeks.AddRange(evenWeek, oddWeek);
        _db.Semesters.Add(semester);

        await AddLessonsForWeek(request.Schedule.EvenWeek.GroupsSchedule, evenWeek, faculty.TimeTable, cancellationToken);
        await AddLessonsForWeek(request.Schedule.OddWeek.GroupsSchedule, oddWeek, faculty.TimeTable, cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task AddLessonsForWeek(IEnumerable<GroupScheduleDto> groupsSchedule, Week week, TimeTable facultyTimeTable, CancellationToken cancellationToken)
    {
        var days = new List<Day>();
        var lessons = new List<Lesson>();

        foreach (var groupSchedule in groupsSchedule)
        {
            foreach (var daySchedule in groupSchedule.Days)
            {
                var day = new Day
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = daySchedule.Day,
                    WeekId = week.Id,
                    Lessons = []
                };

                foreach (var lessonData in daySchedule.Lessons)
                {
                    var lesson = new Lesson
                    {
                        Id = Guid.NewGuid(),
                        GroupId = groupSchedule.GroupId,
                        LessonNumber = lessonData.LessonNumber,
                        TeacherId = lessonData.TeacherId,
                        RoomId = lessonData.RoomId,
                        DayId = day.Id,
                        LessonTimeId = facultyTimeTable.LessonTimes.First(lt => lt.LessonNumber == lessonData.LessonNumber).Id,
                        IsOnline = lessonData.IsOnline ?? false,
                        Url = lessonData.Url ?? null,
                        Type = lessonData.Type ?? LessonType.LectureAndPractice,
                        SubjectId = await GetOrCreateSubjectAsync(lessonData.LessonName, cancellationToken),
                    };

                    day.Lessons.Add(lesson);
                    lessons.Add(lesson);
                }

                days.Add(day);
            }
        }

        week.Days = days;
        await _db.Days.AddRangeAsync(days, cancellationToken);
        await _db.Lessons.AddRangeAsync(lessons, cancellationToken);
    }

    private async Task<Guid> GetOrCreateSubjectAsync(string name, CancellationToken cancellationToken)
    {
        var subject = await _db.Subjects.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        if (subject == null)
        {
            var subjectId = Guid.NewGuid();
            await _db.Subjects.AddAsync(new Subject { Id = subjectId, Name = name }, cancellationToken);

            return subjectId;
        }

        return subject.Id;
    }
}
