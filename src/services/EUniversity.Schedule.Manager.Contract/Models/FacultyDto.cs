namespace EUniversity.Schedule.Manager.Contract.Models;

public class FacultyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }

    public TeacherDto Dean { get; set; }
    public TimeTableDto TimeTable { get; set; }
}
