using EUniversity.Core.Enums;
using EUniversity.Manager.Data.Models.Base;

namespace EUniversity.Manager.Data.Models;

public class Week : BaseEntity
{
    public Guid Id { get; set; }

    public WeekType Type { get; set; }

    public ICollection<Lesson> Lessons { get; set; }

    public Guid ScheduleId { get; set; }
    public virtual Schedule Schedule { get; set; }
}
