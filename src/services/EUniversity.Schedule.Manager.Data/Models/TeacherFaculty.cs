using EUniversity.Schedule.Manager.Data.Models.Base;

namespace EUniversity.Schedule.Manager.Data.Models;

public class TeacherFaculty : BaseEntity
{
    public Guid Id { get; set; }

    public Guid TeacherId { get; set; }
    public virtual Teacher Teacher { get; set; }

    public Guid FacultyId { get; set; }
    public virtual Faculty Faculty { get; set; }
}
