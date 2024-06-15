using EUniversity.Authorization.Data.Enums;

namespace EUniversity.Authorization.Data.Models;

public class UserToken
{
    public Guid Id { get; set; }

    public string Token { get; set; }

    public TokenType TokenType { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ExpiredOn { get; set; }

    public bool IsActive { get; set; } = true;

    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
