namespace EUniversity.Authorization.Data.Models;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Picture { get; set; }
    public string FullName { get; set; }

    public virtual UserRole UserRole { get; set; }
    public virtual ICollection<UserToken> UserTokens { get; set; }
    public virtual ICollection<UserPermission> UserPermissions { get; set; }
}
