namespace EUniversity.Authorization.Contract.Exceptions;

public class InvalidRefreshTokenException(string message) : Exception(message)
{
}
