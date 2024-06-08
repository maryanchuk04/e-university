using EUniversity.Schedule.Manager.Data.Models.Base;

namespace EUniversity.Schedule.Manager.Data.Models;

public class Student : BaseEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid GroupId { get; set; }
    public virtual Group Group { get; set; }
}
