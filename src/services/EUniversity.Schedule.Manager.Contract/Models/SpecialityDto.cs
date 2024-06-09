namespace EUniversity.Schedule.Manager.Contract.Models;

public class SpecialityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid FacultyId { get; set; }
    public string FacultyName { get; set; }
}