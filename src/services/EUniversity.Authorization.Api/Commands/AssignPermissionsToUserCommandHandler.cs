using EUniversity.Authorization.Contract.Exceptions;
using EUniversity.Authorization.Data;
using EUniversity.Authorization.Data.Models;
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

public class AssignPermissionsToUserCommandHandler(AuthorizationDbContext db) : IRequestHandler<AssignPermissionsToUserCommand>
{
    private readonly AuthorizationDbContext _db = db.ThrowIfNull();

    public async Task Handle(AssignPermissionsToUserCommand command, CancellationToken cancellationToken)
    {
        if (command.Permissions.Length == 0 || command.UserId == Guid.Empty)
            return;

        var user = await _db.Users
            .Include(u => u.UserPermissions)
            .ThenInclude(p => p.Permission)
            .SingleOrDefaultAsync(u => u.Id == command.UserId, cancellationToken)
            ?? throw new UserNotFoundException(command.UserId);

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
