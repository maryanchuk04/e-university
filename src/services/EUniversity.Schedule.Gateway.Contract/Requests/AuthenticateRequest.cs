namespace EUniversity.Schedule.Gateway.Contract.Requests;

public class AuthenticateRequest
{
    public string Email { get; set; }

    public bool IsEmailVerified { get; set; }

    public string Picture { get; set; }
}
