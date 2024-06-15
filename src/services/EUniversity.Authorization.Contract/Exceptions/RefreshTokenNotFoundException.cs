namespace EUniversity.Authorization.Contract.Exceptions;

public class RefreshTokenNotFoundException(string message) : Exception(message)
{
}
