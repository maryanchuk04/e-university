using EUniversity.Core.Enums;

namespace EUniversity.Authorization.Contract.Response;

public class UserResponse
{
    public UserResponse() { }
    public UserResponse(Guid id, string email, string picture, IList<string> permissions, Role role)
    {
        Id = id;
        Email = email;
        Picture = picture;
        Permissions = permissions;
        Role = role;
    }

    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Picture { get; set; }

    public IList<string> Permissions { get; set; }
    public Role Role { get; set; }
}
