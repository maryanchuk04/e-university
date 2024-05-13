using EUniversity.Authorization.Data;
using EUniversity.Authorization.Data.Models;
using EUniversity.Shared.Extensions;
using MediatR;

namespace EUniversity.Authorization.Api.Commands;

public class RegisterUserCommand(string email, string picture) : IRequest<Guid>
{
    public string Email { get; set; } = email;
    public string Picture { get; set; } = picture;
}

public class RegisterUserCommandHandler(ILogger<RegisterUserCommandHandler> logger, AuthorizationDbContext db) : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly ILogger<RegisterUserCommandHandler> _logger = logger.ThrowIfNull();
    private readonly AuthorizationDbContext _db = db.ThrowIfNull();

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting register new user with email = '{Email}'", request.Email);

        if (string.IsNullOrEmpty(request.Email))
        {
            _logger.LogError("Email should be provided for registration request!");
            throw new Exception("");
        }

        try
        {
            var user = new User
            {
                Email = request.Email,
                Picture = request.Picture,
            };

            // TODO: Add Some logic with separation to different groups...

            var userId = (await _db.Users.AddAsync(user, cancellationToken)).Entity.Id;

            await _db.UserRoles.AddAsync(new UserRole
            {
                UserId = userId,
                RoleId = Core.Enums.Role.User,
            }, cancellationToken);

            await _db.UserPermissions.AddAsync(new UserPermission
            {
                UserId = user.Id,
                PermissionId = _db.Permissions.Single(p => p.Type == PermissionType.NoAccess).Id
            }, cancellationToken);

            await _db.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully register new user with email = '{Email}'", request.Email);
            return userId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something happened during registering new user with email = '{Email}'", request.Email);
            throw;
        }
    }
}
