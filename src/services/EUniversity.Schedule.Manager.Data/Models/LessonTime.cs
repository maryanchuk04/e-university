using EUniversity.Schedule.Manager.Data.Models.Base;

namespace EUniversity.Schedule.Manager.Data.Models;

public class LessonTime : BaseEntity
{
    public Guid Id { get; set; }
    public int LessonNumber { get; set; }

    public TimeOnly StartAt { get; set; }
    public TimeOnly EndAt { get; set; }

    public Guid TimeTableId { get; set; }
    public virtual TimeTable TimeTable { get; set; }

    public ICollection<Lesson> Lessons { get; set; }
}