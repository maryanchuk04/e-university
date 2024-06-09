namespace EUniversity.Authorization.Contract.Requests;

public class AuthenticateRequest
{
    public AuthenticateRequest() { }

    public AuthenticateRequest(string email, string picture, string fullName)
    {
        Email = email;
        Picture = picture;
        FullName = fullName;
    }

    public string Email { get; set; }

    public string Picture { get; set; }

    public string FullName { get; set; }
}
