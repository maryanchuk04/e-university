namespace EUniversity.Schedule.Manager.Contract.Models.SemesterSchedule;

public class GroupScheduleDto
{
    public Guid GroupId { get; set; }
    public List<DayScheduleDto> Days { get; set; }
}
