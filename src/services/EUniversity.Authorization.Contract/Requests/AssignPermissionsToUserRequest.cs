using EUniversity.Core.Enums;

namespace EUniversity.Authorization.Contract.Requests;

public class AssignPermissionsToUserRequest
{
    public PermissionType[] Permissions { get; set; }
}
