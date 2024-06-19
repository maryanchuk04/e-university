using EUniversity.Authorization.Contract.Response;
using EUniversity.Authorization.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Authorization.Api.Queries.Users;

public class GetUsersQuery : IRequest<List<UserResponse>> { }

public class GetUsersQueryHandler(AuthorizationDbContext db) : IRequestHandler<GetUsersQuery, List<UserResponse>>
{
    public Task<List<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return db.Users
            .Include(u => u.UserPermissions)
            .ThenInclude(p => p.Permission)
            .Include(u => u.UserRole)
            .Select(user => new UserResponse(
                user.Id,
                user.Email,
                user.Picture,
                user.UserPermissions.Select(p => p.Permission.Name).ToList(),
                user.UserRole.RoleId,
                user.FullName))
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
