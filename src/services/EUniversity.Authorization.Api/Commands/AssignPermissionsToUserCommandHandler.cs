using EUniversity.Authorization.Contract.Exceptions;
using EUniversity.Authorization.Data;
using EUniversity.Authorization.Data.Models;
using EUniversity.Core.Enums;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Authorization.Api.Commands;

public class AssignPermissionsToUserCommand(Guid userId, PermissionType[] permissions)
    : IRequest
{
    public Guid UserId { get; set; } = userId;
    public PermissionType[] Permissions { get; set; } = permissions;
}

public class AssignPermissionsToUserCommandHandler(AuthorizationDbContext db, ILogger<AssignPermissionsToUserCommandHandler> logger) : IRequestHandler<AssignPermissionsToUserCommand>
{
    private readonly AuthorizationDbContext _db = db.ThrowIfNull();
    private readonly ILogger<AssignPermissionsToUserCommandHandler> _logger = logger.ThrowIfNull();

    public async Task Handle(AssignPermissionsToUserCommand command, CancellationToken cancellationToken)
    {
        if (command.Permissions.Length == 0 || command.UserId == Guid.Empty)
            return;

        var user = await _db.Users
            .Include(u => u.UserPermissions)
            .ThenInclude(p => p.Permission)
            .SingleOrDefaultAsync(u => u.Id == command.UserId, cancellationToken)
            ?? throw new UserNotFoundException(command.UserId);

        if (command.Permissions.Any(p => p == PermissionType.NoAccess))
        {
            _logger.LogInformation("Reset Permissions to NoAccess for UserId = '{UserId}'", user.Id);
            // that means reset permision to NoAccess
            user.UserPermissions =
            [
                new()
                {
                    PermissionId = _db.Permissions.First(p => p.Type == PermissionType.NoAccess).Id, UserId = user.Id
                }
            ];

            _db.UserPermissions.UpdateRange(user.UserPermissions);
            await _db.SaveChangesAsync(cancellationToken);

            return;
        }

        if (command.Permissions.Any(p => p == PermissionType.FullAccess))
        {
            _logger.LogInformation("Set FullAccess permission for UserId = '{UserId}'", user.Id);
            // that means this user should be ADMIN.
            user.UserPermissions =
            [
                new()
                {
                    PermissionId = _db.Permissions.First(p => p.Type == PermissionType.FullAccess).Id, UserId = user.Id
                }
            ];

            _db.UserPermissions.UpdateRange(user.UserPermissions);
            await _db.SaveChangesAsync(cancellationToken);

            return;
        }

        var userPermissionTypes = user.UserPermissions.Select(p => p.Permission.Type);

        var notAssignedPermissionType = command.Permissions.Where(permissionType => !userPermissionTypes.Contains(permissionType)).ToList();

        if (notAssignedPermissionType == null || notAssignedPermissionType.Count == 0)
            return;

        var notAssignedPermissions = await _db.Permissions.Where(p => notAssignedPermissionType.Contains(p.Type))
            .Select(p => p.Id)
            .ToListAsync(cancellationToken);

        await _db.UserPermissions.AddRangeAsync(
            notAssignedPermissions.Select(p => new UserPermission { PermissionId = p, UserId = user.Id }), cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);
    }
}
