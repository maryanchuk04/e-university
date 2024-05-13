using EUniversity.Authorization.Api.Queries;
using EUniversity.Authorization.Contract.Exceptions;
using EUniversity.Authorization.Contract.Requests;
using EUniversity.Authorization.Contract.Response;
using EUniversity.Authorization.Contract.Services;
using EUniversity.Authorization.Data;
using EUniversity.Authorization.Data.Models;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Authorization.Api.Commands;

public class AuthenticateUserCommand(AuthenticateRequest request) : IRequest<AuthenticateResponse>
{
    public string Email { get; set; } = request.Email;
    public string Picture { get; set; } = request.Picture;
}

public class AuthenticateUserCommandHandler(
    IMediator mediator,
    ITokenGenerator tokenGenerator,
    AuthorizationDbContext db,
    ILogger<AuthenticateUserCommandHandler> logger,
    IPermissionsService permissionsService)
    : IRequestHandler<AuthenticateUserCommand, AuthenticateResponse>
{
    private readonly IMediator _mediator = mediator.ThrowIfNull();
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator.ThrowIfNull();
    private readonly AuthorizationDbContext _db = db.ThrowIfNull();
    private readonly ILogger<AuthenticateUserCommandHandler> _logger = logger.ThrowIfNull();
    private readonly IPermissionsService _permissionsService = permissionsService.ThrowIfNull();

    public async Task<AuthenticateResponse> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        Guid userId = Guid.Empty;

        if (!await _mediator.Send(new CheckIfUserExistQuery(command.Email), cancellationToken))
        {
            // Register user if it not already exist.
            userId = await _mediator.Send(new RegisterUserCommand(command.Email, command.Picture), cancellationToken);
        }

        _logger.LogInformation("Starting login for new user with email = '{Email}'", command.Email);

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
            .Distinct()
            .ToListAsync(cancellationToken: cancellationToken);

        var userPermissions = await _db.UserPermissions
            .Include(p => p.Permission)
            .Where(p => p.UserId == user.Id)
            .Select(p => p.Permission.Name)
            .ToListAsync(cancellationToken: cancellationToken);

        var accessToken = _tokenGenerator.GenerateAccessToken(user.Id, user.Email, userRoles, userPermissions);
        var refreshToken = _tokenGenerator.GenerateRefreshToken();

        await _db.UserTokens.AddAsync(new UserToken
        {
            Token = refreshToken,
            TokenType = Data.Enums.TokenType.RefreshToken,
            CreatedOn = DateTime.UtcNow,
            ExpiredOn = DateTime.UtcNow.AddDays(15),
            IsActive = true,
        }, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return new AuthenticateResponse(user.Id, accessToken, refreshToken);
    }
}
