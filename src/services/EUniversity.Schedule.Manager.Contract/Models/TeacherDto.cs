namespace EUniversity.Schedule.Manager.Contract.Models;

public class TeacherDto
{
    public TeacherDto() { }

    public TeacherDto(Guid id, string fullName, string position)
    {
        Id = id;
        FullName = fullName;
        Position = position;
    }

    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Position { get; set; }
    public Guid UserId { get; set; }
    public List<FacultyDto> Faculties { get; set; }
}
