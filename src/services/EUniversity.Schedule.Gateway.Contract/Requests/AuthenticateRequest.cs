namespace EUniversity.Schedule.Gateway.Contract.Requests;

public class AuthenticateRequest
{
    public string Email { get; set; }

    public string Picture { get; set; }

    public string FullName { get; set; }
}
