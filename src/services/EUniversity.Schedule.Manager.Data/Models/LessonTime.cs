namespace EUniversity.Schedule.Manager.Data.Models;

public class LessonTime
{
    public Guid Id { get; set; }
    public int LessonNumber { get; set; }

    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }

    public Guid TimeTableId { get; set; }
    public virtual TimeTable TimeTable { get; set; }

    public ICollection<Lesson> Lessons { get; set; }
}