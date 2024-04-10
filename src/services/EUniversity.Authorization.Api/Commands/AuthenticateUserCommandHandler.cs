using EUniversity.Authorization.Api.Queries;
using EUniversity.Authorization.Contract.Exceptions;
using EUniversity.Authorization.Contract.Requests;
using EUniversity.Authorization.Contract.Services;
using EUniversity.Authorization.Data;
using EUniversity.Authorization.Data.Models;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Authorization.Api.Commands;

public class AuthenticateUserCommand(AuthenticateRequest request) : IRequest
{
    public string Email { get; set; } = request.Email;
    public string Picture { get; set; } = request.Picture;
}

public class AuthenticateUserCommandHandler(IMediator mediator, ITokenGenerator tokenGenerator, AuthorizationDbContext db) : IRequestHandler<AuthenticateUserCommand>
{
    private readonly IMediator _mediator = mediator.ThrowIfNull();
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator.ThrowIfNull();
    private readonly AuthorizationDbContext _db = db.ThrowIfNull();

    public async Task Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        Guid userId = Guid.Empty;

        if (!await _mediator.Send(new CheckIfUserExistQuery(command.Email), cancellationToken))
        {
            // Register user if it not already exist.
            userId = await _mediator.Send(new RegisterUserCommand(command.Email, command.Picture), cancellationToken);
        }

        User user;
        if (userId == Guid.Empty)
        {
            user = await db.Users.FirstOrDefaultAsync(u => u.Email == command.Email, cancellationToken)
                ?? throw new UserNotFoundException(command.Email);
        }
        else
        {
            user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken)
               ?? throw new UserNotFoundException(userId);
        }

        var userRoles = await _db.UserRoles
            .Where(ur => ur.UserId == user.Id)
            .Select(ur => ur.RoleId)
            .ToListAsync(cancellationToken: cancellationToken);

        var accessToken = _tokenGenerator.GenerateAccessToken(user.Id, user.Email, userRoles);
    }
} 
