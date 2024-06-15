using EUniversity.Authorization.Client;
using EUniversity.Schedule.Gateway.Contract.Models;
using EUniversity.Schedule.Gateway.Contract.Responses;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Commands.Authentication;

public class RefreshAccessTokenCommand(string refreshToken) : IRequest<AuthenticateResponse>
{
    public string RefreshToken { get; set; } = refreshToken;
}

public class RefreshAccessTokenCommandHandler(IAuthorizationClient authorizationClient) : IRequestHandler<RefreshAccessTokenCommand, AuthenticateResponse>
{
    public async Task<AuthenticateResponse> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var res = await authorizationClient.RefreshTokenAsync(request.RefreshToken, cancellationToken);

        return new AuthenticateResponse(res.AccessToken, res.RefreshToken, res.UserId);
    }
}
