using EUniversity.Authorization.Contract.Exceptions;
using EUniversity.Authorization.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Authorization.Api.Commands;

internal class DeleteUserCommand(Guid userId) : IRequest<Unit>
{
    public Guid UserId { get; set; } = userId;
}

internal class DeleteUserCommandHandler(AuthorizationDbContext db) : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await db.Users
            .Include(u => u.UserRole)
            .Include(u => u.UserPermissions)
            .Include(u => u.UserTokens)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken) 
            ?? throw new UserNotFoundException(request.UserId);

        db.Remove(user.UserRole);
        db.RemoveRange(user.UserPermissions);
        db.Remove(user);

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
