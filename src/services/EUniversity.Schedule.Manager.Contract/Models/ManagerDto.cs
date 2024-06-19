namespace EUniversity.Schedule.Manager.Contract.Models;

public class ManagerDto
{
    public Guid ManagerId { get; set; }
    public Guid UserId { get; set; }

    public Guid? StundentId { get; set; }
    public Guid? TeacherId { get; set; }
    public Guid? FacultyId { get; set; }

    public string FacultyName { get; set; }
}
