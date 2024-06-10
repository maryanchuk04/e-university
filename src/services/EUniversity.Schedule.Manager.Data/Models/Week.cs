using EUniversity.Core.Enums;
using EUniversity.Schedule.Manager.Data.Models.Base;

namespace EUniversity.Schedule.Manager.Data.Models;

public class Week : BaseEntity
{
    public Guid Id { get; set; }

    public WeekType Type { get; set; }

    public ICollection<Day> Days { get; set; }
}