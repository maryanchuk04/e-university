namespace EUniversity.Authorization.Contract.Requests;

public class RegisterRequest
{
    public string Email { get; set; }

    /// <summary>
    /// Organization domain
    /// </summary>
    public string Hd { get; set; }
}
