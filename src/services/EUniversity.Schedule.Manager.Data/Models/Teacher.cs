using EUniversity.Schedule.Manager.Data.Models.Base;

namespace EUniversity.Schedule.Manager.Data.Models;

public class Teacher : BaseEntity
{
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public string Position { get; set; }

    /// <summary>
    /// Reference to Auth Service User
    /// </summary>
    public Guid UserId { get; set; }

    public ICollection<Lesson> Lessons { get; set; }

    public bool IsDisabled { get; set; } = false;
}
