using EUniversity.Core.Extensions;
using EUniversity.Schedule.Gateway.Contract.Providers;
using EUniversity.Schedule.Gateway.Contract.Responses;
using EUniversity.Schedule.Manager.Client;
using EUniversity.Schedule.Manager.Contract.Extensions;
using EUniversity.Schedule.Manager.Contract.Models.SemesterSchedule;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Queries.Users.Students;

public class GetStudentDayQuery : IRequest<MyDayGatewayView>
{
}

public class GetStudentDayQueryHandler(
    IPortalUserProvider userProvider,
    IScheduleManagerClient scheduleManagerClient) : IRequestHandler<GetStudentDayQuery, MyDayGatewayView>
{
    public async Task<MyDayGatewayView> Handle(GetStudentDayQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userProvider.GetPortalUser();

        var student = await scheduleManagerClient.GetStudentInfoByUserIdAsync(currentUser.UserId, cancellationToken);

        var schedule = await scheduleManagerClient.GetScheduleAsync(student.FacultyId, cancellationToken);

        var currentWeek = Core.Enums.WeekType.Even;
        var today = DateTime.Today.GetDayOfWeek();
        var dayLessons = new List<LessonDto>();

        if (currentWeek == Core.Enums.WeekType.Even)
        {
            var day = schedule.EvenWeek.GroupsSchedule
                .First(gs => gs.GroupId == student.GroupId).Days.FirstOrDefault(d => d.Day == today);

            dayLessons = day is null ? [] : day.Lessons;
        }
        else
        {
            var day = schedule.OddWeek.GroupsSchedule
                .First(gs => gs.GroupId == student.GroupId).Days.FirstOrDefault(d => d.Day == today);

            dayLessons = day is null ? [] : day.Lessons;
        }

        var myDayGatewayView = new MyDayGatewayView(DateTime.Today, Core.Enums.WeekType.Even, dayLessons, today);

        // Find next day with lessons
        if (dayLessons.Count == 0)
        {
            if (today is Core.Enums.DayOfWeek.Saturday or Core.Enums.DayOfWeek.Sunday)
            {
                DayScheduleDto? day;

                day = currentWeek == Core.Enums.WeekType.Even
                    ? schedule.OddWeek.GroupsSchedule.GetNextDayWithLessonsForGroup(student.GroupId)
                    : schedule.EvenWeek.GroupsSchedule.GetNextDayWithLessonsForGroup(student.GroupId);

                if (day is null)
                    return myDayGatewayView;

                myDayGatewayView.NextDayLessons = day.Lessons;
                myDayGatewayView.NextDayWeekType = currentWeek == Core.Enums.WeekType.Even ? Core.Enums.WeekType.Odd : Core.Enums.WeekType.Even;
                myDayGatewayView.NextDay = day.Day;
            }
        }

        return myDayGatewayView;
    }
}
