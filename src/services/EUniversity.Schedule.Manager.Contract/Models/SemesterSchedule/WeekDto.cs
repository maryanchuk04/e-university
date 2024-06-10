namespace EUniversity.Schedule.Manager.Contract.Models.SemesterSchedule;

public class WeekDto
{
    public Guid WeekId { get; set; }
    public List<GroupScheduleDto> GroupsSchedule { get; set; }
}