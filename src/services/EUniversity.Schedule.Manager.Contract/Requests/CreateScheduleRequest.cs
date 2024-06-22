namespace EUniversity.Schedule.Manager.Contract.Requests;

public class CreateScheduleRequest
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
}
