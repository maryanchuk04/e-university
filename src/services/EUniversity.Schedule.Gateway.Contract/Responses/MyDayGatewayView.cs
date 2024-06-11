using System;
using EUniversity.Core.Enums;
using EUniversity.Schedule.Manager.Contract.Models.SemesterSchedule;

namespace EUniversity.Schedule.Gateway.Contract.Responses;

public class MyDayGatewayView
{
    public MyDayGatewayView() { }

    public MyDayGatewayView(DateTime date, WeekType currentWeekType, List<LessonDto> lessons)
    {
        Date = date;
        CurrentWeekType = currentWeekType;
        Lessons = lessons;
    }

    public DateTime Date { get; set; } = DateTime.Now.Date;
    public WeekType CurrentWeekType { get; set; }
    public List<LessonDto> Lessons { get; set; }
}
