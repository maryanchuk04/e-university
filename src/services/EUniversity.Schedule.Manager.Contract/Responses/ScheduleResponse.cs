using EUniversity.Schedule.Manager.Contract.Models.SemesterSchedule;

namespace EUniversity.Schedule.Manager.Contract.Responses;

public class ScheduleResponse
{
    public Guid SemesterId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public WeekDto EvenWeek { get; set; }
    public WeekDto OddWeek { get; set; }
}