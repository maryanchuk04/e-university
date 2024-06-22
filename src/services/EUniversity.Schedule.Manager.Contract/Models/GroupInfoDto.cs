namespace EUniversity.Schedule.Manager.Contract.Models;

public class GroupInfoDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string SpecialityName { get; set; }
    public Guid FacultyId { get; set; }
}
