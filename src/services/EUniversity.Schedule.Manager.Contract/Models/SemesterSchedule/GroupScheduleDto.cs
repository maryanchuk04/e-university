namespace EUniversity.Schedule.Manager.Contract.Models.SemesterSchedule;

public class GroupScheduleDto
{
    public Guid Id { get; set; }
    public List<DayScheduleDto> Days { get; set; }
}
