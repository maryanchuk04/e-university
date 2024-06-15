using EUniversity.Authorization.Contract.Exceptions;
using EUniversity.Authorization.Contract.Response;
using EUniversity.Authorization.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Authorization.Api.Commands;

public class RefreshTokenCommand(string refreshToken) : IRequest<AuthenticateResponse>
{
    public string RefreshToken { get; set; } = refreshToken;
}

public class RefreshTokenCommandHandler(AuthorizationDbContext db, IMediator mediator)
    : IRequestHandler<RefreshTokenCommand, AuthenticateResponse>
{
    public Task<AuthenticateResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var refreshToken = command.RefreshToken;

        var token = db.UserTokens
            .Include(t => t.User)
            .FirstOrDefault(t => t.Token == refreshToken)
            ?? throw new RefreshTokenNotFoundException("Refresh token was not found!");

        if (DateTime.UtcNow > token.ExpiredOn)
            throw new InvalidRefreshTokenException("Refresh token was expired!");

        if (!token.IsActive)
            throw new InvalidRefreshTokenException("Refresh token in inactive state!");

        token.IsActive = false;
        db.UserTokens.Update(token);

        return mediator.Send(new AuthenticateUserCommand(token.User.Email), cancellationToken);
    }
}
