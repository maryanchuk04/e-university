namespace EUniversity.Authorization.Contract.Response;

public class AuthenticateResponse
{
    public AuthenticateResponse() { }

    public AuthenticateResponse(Guid userId, string accessToken, string refreshToken)
    {
        UserId = userId;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public Guid UserId { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
