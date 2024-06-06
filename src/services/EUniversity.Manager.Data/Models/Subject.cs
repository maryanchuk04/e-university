using EUniversity.Manager.Data.Models.Base;

namespace EUniversity.Manager.Data.Models;

public class Subject : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public bool IsDisabled { get; set; } = false;
}
