namespace EUniversity.Authorization.Data.Models;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Picture { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual ICollection<UserToken> UserTokens { get; set; }
}
