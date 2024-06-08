using EUniversity.Schedule.Manager.Data.Models.Base;

namespace EUniversity.Schedule.Manager.Data.Models;

public class Faculty : BaseEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Adress { get; set; }

    public Guid DeanId { get; set; }
    public virtual Teacher Dean { get; set; }

    public Guid TimeTableId { get; set; }
    public virtual TimeTable TimeTable { get; set; }

    public ICollection<Speciality> Specialities { get; set; }
    public ICollection<Room> Rooms { get; set; }
    public ICollection<TeacherFaculty> TeacherFaculties { get; set; }
}
