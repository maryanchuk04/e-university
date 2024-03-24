namespace EUniversity.Authorization.Contract.Requests;

public class AuthenticateRequest
{
    public AuthenticateRequest() { }

    public AuthenticateRequest(string email, bool isEmailVerified, string picture)
    {
        Email = email;
        IsEmailVerified = isEmailVerified;
        Picture = picture;
    }

    public string Email { get; set; }

    public bool IsEmailVerified { get; set; }

    public string Picture { get; set; }
}
