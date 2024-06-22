using EUniversity.Schedule.Manager.Contract.Models;

namespace EUniversity.Schedule.Gateway.Contract.Responses;

public class TeacherResponse : TeacherDto
{
    public string Picture { get; set; }
    public bool IsActive { get; set; }
}
