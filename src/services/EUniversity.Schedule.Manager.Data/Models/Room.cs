using EUniversity.Schedule.Manager.Data.Models.Base;

namespace EUniversity.Schedule.Manager.Data.Models;

public class Room : BaseEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid FacultyId { get; set; }

    public Faculty Faculty { get; set; }

    public bool IsDisabled { get; set; }
}
