using EUniversity.Core.Enums;

namespace EUniversity.Schedule.Gateway.Contract.Requests;

public class CreateStudentRequest
{
    public string Email { get; set; }
    public Guid GroupId { get; set; }
    public Guid FacultyId { get; set; }
    public PermissionType[] Permissions { get; set; }
}
