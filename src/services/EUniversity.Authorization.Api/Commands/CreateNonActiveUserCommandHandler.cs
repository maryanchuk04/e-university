using EUniversity.Authorization.Contract.Requests;
using EUniversity.Authorization.Data;
using EUniversity.Authorization.Data.Models;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Authorization.Api.Commands;

public class CreateNonActiveUserCommand(CreateUserRequest createUserRequest) : IRequest<Guid>
{
    public CreateUserRequest User { get; set; } = createUserRequest;
}

public class CreateNonActiveUserCommandHandler(AuthorizationDbContext db) : IRequestHandler<CreateNonActiveUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateNonActiveUserCommand command, CancellationToken cancellationToken)
    {
        command.ThrowIfNull().User.ThrowIfNull();

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.User.Email,
            IsActive = false,
        };

        var userRole = new UserRole { RoleId = command.User.Role, UserId = user.Id };
        var usersPermissions = new List<UserPermission>();

        var requestedPermissionsIds = await db.Permissions
            .Where(p => command.User.Permissions.Contains(p.Type)).Select(x => x.Id)
            .ToListAsync(cancellationToken);

        foreach (var permissionId in requestedPermissionsIds)
        {
            usersPermissions.Add(new UserPermission { UserId = user.Id, PermissionId = permissionId });
        }

        user.UserRole = userRole;
        user.UserPermissions = usersPermissions;

        await db.Users.AddAsync(user, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
