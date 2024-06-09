namespace EUniversity.Schedule.Gateway.Contract.Models;

public class StudentGatewayView
{
    public Guid StudentId { get; set; }
    public Guid UserId { get; set; }

    // from auth service
    public string FullName { get; set; }
    public string Picture { get; set; }

    // from manager
    public Guid GroupId { get; set; }
    public string GroupName { get; set; }
    public Guid FacultyId { get; set; }
    public string FacultyName { get; set; }
    public Guid SpecialityId { get; set; }
    public string SpecialityName { get; set; }
}
