using EUniversity.Authorization.Data.Models;

namespace EUniversity.Authorization.Contract.Requests;

public class AssignPermissionsToUserRequest
{
    public PermissionType[] Permissions { get; set; }
}
