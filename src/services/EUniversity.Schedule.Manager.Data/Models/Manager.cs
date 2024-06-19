using EUniversity.Schedule.Manager.Data.Models.Base;

namespace EUniversity.Schedule.Manager.Data.Models;

public class Manager : BaseEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid? FacultyId { get; set; }
    public virtual Faculty? Faculty { get; set; }

    public Guid? StundentId { get; set; }
    public Guid? TeacherId { get; set; }

    public virtual Teacher? Teacher { get; set; }
    public virtual Student? Student { get; set; }
}
