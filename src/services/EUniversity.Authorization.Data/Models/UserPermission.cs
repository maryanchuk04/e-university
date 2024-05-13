namespace EUniversity.Authorization.Data.Models;

public class UserPermission
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}
