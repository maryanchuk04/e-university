using EUniversity.Core.Enums;

namespace EUniversity.Schedule.Gateway.Contract.Models;

public interface IPortalUser
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public IList<PermissionType> Permissions { get; set; }
}

public class PortalUser : IPortalUser
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public IList<PermissionType> Permissions { get; set; }
}
