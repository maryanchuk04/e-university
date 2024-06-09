namespace EUniversity.Schedule.Manager.Contract.Requests;

public class CreateSpecialityRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid FacultyId { get; set; }
}

