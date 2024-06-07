using EUniversity.Schedule.Manager.Data.Models.Base;

namespace EUniversity.Schedule.Manager.Data.Models;

public class Semester : BaseEntity
{
    public Guid Id { get; set; }

    public Guid ScheduleId { get; set; }
    public virtual Schedule Schedule { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
