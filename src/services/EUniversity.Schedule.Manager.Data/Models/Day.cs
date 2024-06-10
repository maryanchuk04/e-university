using EUniversity.Schedule.Manager.Data.Models.Base;

namespace EUniversity.Schedule.Manager.Data.Models;

public class Day : BaseEntity
{
    public Guid Id { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public Guid WeekId { get; set; }
    public Week Week { get; set; }

    public ICollection<Lesson> Lessons { get; set; }
}
