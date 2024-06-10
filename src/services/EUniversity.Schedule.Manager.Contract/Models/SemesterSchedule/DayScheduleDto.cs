namespace EUniversity.Schedule.Manager.Contract.Models.SemesterSchedule;

public class DayScheduleDto
{
    public DayOfWeek Day { get; set; }
    public List<LessonDto> Lessons { get; set; }
}
