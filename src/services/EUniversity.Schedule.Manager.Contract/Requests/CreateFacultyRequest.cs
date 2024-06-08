namespace EUniversity.Schedule.Manager.Contract.Requests;

public class CreateFacultyRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public Guid DeanId { get; set; }
}
