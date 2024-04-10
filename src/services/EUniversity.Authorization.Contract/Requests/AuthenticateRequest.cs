namespace EUniversity.Authorization.Contract.Requests;

public class AuthenticateRequest
{
    public AuthenticateRequest() { }

    public AuthenticateRequest(string email, string picture)
    {
        Email = email;
        Picture = picture;
    }

    public string Email { get; set; }

    public string Picture { get; set; }
}
