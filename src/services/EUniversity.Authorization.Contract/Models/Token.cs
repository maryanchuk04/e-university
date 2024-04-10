namespace EUniversity.Authorization.Contract.Models;

public class Token
{
    public Token() { }

    public Token(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
