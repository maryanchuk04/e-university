using EUniversity.Manager.Data.Models.Base;

namespace EUniversity.Manager.Data.Models;

public class Speciality : BaseEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid FacultyId { get; set; }
    public Faculty Faculty { get; set; }
}
