namespace EUniversity.Schedule.Manager.Contract.Requests;

public class CreateStudentRequest
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    public Guid FacultyId { get; set; }
}
