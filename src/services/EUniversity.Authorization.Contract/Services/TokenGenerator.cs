using EUniversity.Authorization.Data.Models;

namespace EUniversity.Authorization.Contract.Services;

public interface ITokenGenerator
{
    string GenerateAccessToken(UserRole userRole);
}


public class TokenGenerator : ITokenGenerator
{
    public string GenerateAccessToken(UserRole userRole)
    {
        throw new NotImplementedException();
    }
}
