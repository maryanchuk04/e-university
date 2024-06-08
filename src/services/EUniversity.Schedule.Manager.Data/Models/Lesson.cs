using EUniversity.Core.Enums;
using EUniversity.Schedule.Manager.Data.Models.Base;

namespace EUniversity.Schedule.Manager.Data.Models;

public class Lesson : BaseEntity
{
    public Guid Id { get; set; }

    public int LessonNumber { get; set; }

    public bool IsOnline { get; set; }

    public string Url { get; set; }

    public LessonType Type { get; set; }

    public Guid WeekId { get; set; }
    public virtual Week Week { get; set; }

    public Guid LessonTimeId { get; set; }
    public LessonTime LessonTime { get; set; }

    public Guid GroupId { get; set; }
    public virtual Group Group { get; set; }

    public Guid? RoomId { get; set; }
    public virtual Room Room { get; set; }

    public Guid TeacherId { get; set; }
    public virtual Teacher Teacher { get; set; }

    public Guid SubjectId { get; set; }
    public virtual Subject Subject { get; set; }
}
