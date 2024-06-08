namespace EUniversity.Schedule.Manager.Contract.Exceptions;

public class EntityAlreadyExistException(string entityName, string key)
    : Exception($"Entity = '{entityName}' already exist in database with key = '{key}'")
{
}
