using EUniversity.Schedule.Manager.Data.Models.Base;

namespace EUniversity.Schedule.Manager.Data.Models;

/// <summary>
/// Each Faculty can have different time table.
/// </summary>
public class TimeTable : BaseEntity
{
    public Guid Id { get; set; }

    public ICollection<LessonTime> LessonTimes { get; set; }

    public Guid FacultyId { get; set; }
    public virtual Faculty Faculty { get; set; }
}
