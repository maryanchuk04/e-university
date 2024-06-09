namespace EUniversity.Schedule.Manager.Contract.Models;

public class GroupDto
{
    public Guid Id { get; set; }
    public int Course { get; set; }
    public string Name { get; set; }
    public Guid FacultyId { get; set; }
    public string FacultyName { get; set; }
    public Guid SpecialityId { get; set; }
    public string SpecialityName { get; set; }
    public Guid? HeadStudentId { get; set; }
    public string HeadStudentName { get; set; }
    public Guid? CuratorId { get; set; }
    public string CuratorName { get; set; }
    public bool IsDisabled { get; set; }
    public List<StudentDto> Students { get; set; }
    public List<LessonDto> Lessons { get; set; }
}