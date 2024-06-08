using EUniversity.Schedule.Manager.Data.Models.Base;

namespace EUniversity.Schedule.Manager.Data.Models;

public class Subject : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public bool IsDisabled { get; set; } = false;

    public ICollection<Lesson> Lessons { get; set; }
}
