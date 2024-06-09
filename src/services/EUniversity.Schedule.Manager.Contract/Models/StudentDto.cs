namespace EUniversity.Schedule.Manager.Contract.Models;

public class StudentDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    public string GroupName { get; set; }
    public Guid FacultyId { get; set; }
    public string FacultyName { get; set; }
    public Guid SpecialityId { get; set; }
    public string SpecialityName { get; set; }
    public Guid? HeadStudentId { get; set; }
    public string HeadStudentName { get; set; }
    public Guid? CuratorId { get; set; }
    public string CuratorName { get; set; }
}