using EUniversity.Manager.Data.Models.Base;

namespace EUniversity.Manager.Data.Models;

public class Teacher : BaseEntity
{
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public bool IsDisabled { get; set; } = false;
}
