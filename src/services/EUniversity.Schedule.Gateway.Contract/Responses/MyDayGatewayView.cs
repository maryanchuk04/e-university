using EUniversity.Core.Enums;
using EUniversity.Schedule.Manager.Contract.Models.SemesterSchedule;


namespace EUniversity.Schedule.Gateway.Contract.Responses;

public class MyDayGatewayView
{
    public MyDayGatewayView() { }

    public MyDayGatewayView(DateTime date, WeekType currentWeekType, List<LessonDto> lessons, Core.Enums.DayOfWeek today)
    {
        Date = date;
        CurrentWeekType = currentWeekType;
        Lessons = lessons;
        Today = today;
    }

    public DateTime Date { get; set; } = DateTime.Now.Date;
    public Core.Enums.DayOfWeek Today { get; set; }
    public WeekType CurrentWeekType { get; set; }
    public List<LessonDto> Lessons { get; set; }

    // This section of properties needed in case Today student do not have lessons
    public Core.Enums.DayOfWeek NextDay { get; set; }
    public List<LessonDto> NextDayLessons { get; set; }
    public WeekType NextDayWeekType { get; set; }
}
