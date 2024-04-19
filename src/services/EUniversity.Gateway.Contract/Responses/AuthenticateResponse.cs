namespace EUniversity.Gateway.Contract.Responses;

public class AuthenticateResponse
{
    public AuthenticateResponse() { }

    public AuthenticateResponse(string accessToken, string refreshToken, Guid userId)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        UserId = userId;
    }

    public string AccessToken  { get; set; }

    public string RefreshToken { get; set; }

    public Guid UserId { get; set; }
}
