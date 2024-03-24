namespace EUniversity.Gateway.Contract.Responses;

public class AuthenticateResponse
{
    public string AccessToken  { get; set; }

    public string RefreshToken { get; set; }
}
