using EUniversity.Authorization.Data;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Authorization.Api.Queries.Users;

public class CheckIfUserExistQuery(string email) : IRequest<bool>
{
    public string Email { get; set; } = email;
}

public class CheckIfUserExistQueryHandler(ILogger<CheckIfUserExistQueryHandler> logger, AuthorizationDbContext db)
    : IRequestHandler<CheckIfUserExistQuery, bool>
{
    private readonly AuthorizationDbContext _db = db.ThrowIfNull();
    private readonly ILogger<CheckIfUserExistQueryHandler> _logger = logger.ThrowIfNull();

    public async Task<bool> Handle(CheckIfUserExistQuery query, CancellationToken cancellationToken)
    {
        try
        {
            return await _db.Users.AnyAsync(u => u.Email == query.Email, cancellationToken);
        }
        catch (Exception)
        {
            _logger.LogWarning("Something happened during checking if user = {email} already exist", query.Email);
            return false;
        }
    }
}
