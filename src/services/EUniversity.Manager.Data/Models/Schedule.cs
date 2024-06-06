using EUniversity.Schedule.Manager.Data.Models.Base;

namespace EUniversity.Schedule.Manager.Data.Models;

public class Schedule : BaseEntity
{
    public Guid Id { get; set; }

    public Guid EvenWeekId { get; set; }
    public Week EvenWeek { get; set; }

    public Guid OddWeekId { get; set; }
    public Week OddWeek { get; set; }
}
