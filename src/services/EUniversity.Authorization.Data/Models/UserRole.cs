namespace EUniversity.Authorization.Data.Models;

public class UserRole
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public Core.Enums.Role RoleId { get; set; }
    public virtual Role Role { get; set; }
}
