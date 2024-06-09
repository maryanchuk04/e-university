namespace EUniversity.Schedule.Manager.Contract.Requests;

public class CreateGroupRequest
{
    public int Course { get; set; }
    public string Name { get; set; }
    public Guid FacultyId { get; set; }
    public Guid SpecialityId { get; set; }
    public Guid? HeadStudentId { get; set; }
    public Guid? CuratorId { get; set; }
    public List<Guid>? StudentIds { get; set; }
    public List<Guid>? LessonIds { get; set; }
}