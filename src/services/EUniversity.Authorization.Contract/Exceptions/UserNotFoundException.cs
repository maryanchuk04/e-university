namespace EUniversity.Authorization.Contract.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string email) : base($"User was not found by email = '{email}'") { }

    public UserNotFoundException(Guid id) : base($"User was not found by Id = '{id}'") { }
}
