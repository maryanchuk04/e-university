namespace EUniversity.Schedule.Manager.Contract.Models;

public class FacultyDto
{
    public FacultyDto() { }

    public FacultyDto(Guid id, string name, string description, string address)
    {
        Id = id;
        Name = name;
        Description = description;
        Address = address;
    }

    public FacultyDto(Guid id, string name, string description, string address, TeacherDto dean, TimeTableDto timeTable)
    {
        Id = id;
        Name = name;
        Description = description;
        Address = address;
        Dean = dean;
        TimeTable = timeTable;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }

    public TeacherDto? Dean { get; set; }
    public TimeTableDto? TimeTable { get; set; }
}
