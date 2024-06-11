using EUniversity.Schedule.Gateway.Contract.Providers;
using EUniversity.Schedule.Gateway.Contract.Responses;
using EUniversity.Schedule.Manager.Client;
using EUniversity.Schedule.Manager.Contract.Models.SemesterSchedule;
using EUniversity.Shared.Extensions;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Queries.Users.Students;

public class GetStudentDayQuery : IRequest<MyDayGatewayView>
{
}

public class GetStudentDayQueryHandler(
    IPortalUserProvider userProvider,
    IScheduleManagerClient scheduleManagerClient) : IRequestHandler<GetStudentDayQuery, MyDayGatewayView>
{
    private readonly IPortalUserProvider userProvider = userProvider.ThrowIfNull();
    private readonly IScheduleManagerClient scheduleManagerClient = scheduleManagerClient.ThrowIfNull();

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
            dayLessons = schedule.EvenWeek.GroupsSchedule
                .First(gs => gs.GroupId == student.GroupId)
                .Days
                .First(d => d.Day == today).Lessons;
        }
        else
        {
            dayLessons = schedule.OddWeek.GroupsSchedule
               .First(gs => gs.GroupId == student.GroupId)
               .Days
               .First(d => d.Day == today).Lessons;
        }

        return new MyDayGatewayView(DateTime.Today, Core.Enums.WeekType.Even, dayLessons);
    }
}
