using System.Linq.Expressions;
using EUniversity.Authorization.Contract.Exceptions;
using EUniversity.Authorization.Contract.Response;
using EUniversity.Authorization.Data;
using EUniversity.Authorization.Data.Models;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Authorization.Api.Queries.Users;

public class GetUserQuery : IRequest<UserResponse>
{
    public GetUserQuery(string email)
    {
        Email = email.ToLower();
    }

    public GetUserQuery(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
    public string Email { get; set; }

    public bool IsSearchByEmail => !string.IsNullOrEmpty(Email);
}

public class GetUserQueryHandler(AuthorizationDbContext db) : IRequestHandler<GetUserQuery, UserResponse>
{
    private readonly AuthorizationDbContext _db = db.ThrowIfNull();

    public async Task<UserResponse> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>> searchCondition = query.IsSearchByEmail ? u => u.Email == query.Email : u => u.Id == query.UserId;

        var user = await _db.Users
            .Include(u => u.UserPermissions)
            .ThenInclude(p => p.Permission)
            .Include(u => u.UserRole)
            .SingleOrDefaultAsync(searchCondition, cancellationToken);

        if (user is null)
        {
            var ex = query.IsSearchByEmail
                ? new UserNotFoundException(query.Email)
                : new UserNotFoundException(query.UserId);
            throw ex;
        }

        return new UserResponse(
            user.Id,
            user.Email,
            user.Picture,
            user.UserPermissions.Select(p => p.Permission.Name).ToList(),
            user.UserRole.RoleId);
    }
}
