using EUniversity.Core.Enums;

namespace EUniversity.Authorization.Contract.Requests;

public class CreateUserRequest
{
    public string Email { get; set; }
    public Core.Enums.Role Role { get; set; }
    public PermissionType[] Permissions { get; set; }
}
