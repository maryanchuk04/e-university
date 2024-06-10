namespace EUniversity.Schedule.Manager.Contract.Models.SemesterSchedule;

public class RoomDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid FacultyId { get; set; }
    public string FacultyName { get; set; }
    public bool IsDisabled { get; set; }
}