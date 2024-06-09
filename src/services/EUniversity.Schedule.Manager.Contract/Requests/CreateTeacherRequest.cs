namespace EUniversity.Schedule.Manager.Contract.Requests;

public class CreateTeacherRequest
{
    public string FullName { get; set; }
    public string Position { get; set; }
    public Guid UserId { get; set; }

    public IList<Guid> FacultyIds { get; set; } = [];
}
