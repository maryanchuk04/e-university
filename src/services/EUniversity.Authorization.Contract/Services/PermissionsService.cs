using EUniversity.Authorization.Data;
using EUniversity.Authorization.Data.Models;
using EUniversity.Core.Enums;
using EUniversity.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Authorization.Contract.Services;

public interface IPermissionsService
{
    Task<List<Permission>> DeterminePermissionsByRole(Core.Enums.Role role, CancellationToken cancellationToken = default);
}

public class PermissionsService(AuthorizationDbContext db) : IPermissionsService
{
    public AuthorizationDbContext _db = db.ThrowIfNull();

    public Task<List<Permission>> DeterminePermissionsByRole(Core.Enums.Role role, CancellationToken cancellationToken = default)
    {
        return role switch
        {
            Core.Enums.Role.User => _db.Permissions.Where(p => p.Type == PermissionType.NoAccess).ToListAsync(cancellationToken),
            Core.Enums.Role.Student => _db.Permissions.Where(p => p.Type == PermissionType.ScheduleViewer).ToListAsync(cancellationToken),
            Core.Enums.Role.Teacher => _db.Permissions.Where(p => p.Type == PermissionType.ScheduleViewer).ToListAsync(cancellationToken),
            Core.Enums.Role.ScheduleAdmin => _db.Permissions.Where(p => p.Type == PermissionType.ScheduleEditor).ToListAsync(cancellationToken),
            Core.Enums.Role.FacultyAdmin => _db.Permissions.Where(p => p.Type == PermissionType.FacultyFullAccess).ToListAsync(cancellationToken),
            Core.Enums.Role.Admin => _db.Permissions.Where(p => p.Type == PermissionType.FullAccess).ToListAsync(cancellationToken),
            _ => _db.Permissions.Where(p => p.Type == PermissionType.NoAccess).ToListAsync(cancellationToken)
        };
    }
}
