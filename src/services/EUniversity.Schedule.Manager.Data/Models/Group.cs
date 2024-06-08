using EUniversity.Schedule.Manager.Data.Models.Base;

namespace EUniversity.Schedule.Manager.Data.Models;

public class Group : BaseEntity
{
    public Guid Id { get; set; }

    public int Course { get; set; }

    public string Name { get; set; }

    public Guid FacultyId { get; set; }
    public Faculty Faculty { get; set; }

    public Guid SpecialityId { get; set; }
    public Speciality Speciality { get; set; }

    public Guid HeadStudentId { get; set; }
    public virtual Student HeadStudent { get; set; }

    public Guid CuratorId { get; set; }
    public virtual Teacher Curator { get; set; }

    public ICollection<Student> Students { get; set; }
    public ICollection<Lesson> Lessons { get; set; }

    public bool IsDisabled { get; set; } = false;
}
