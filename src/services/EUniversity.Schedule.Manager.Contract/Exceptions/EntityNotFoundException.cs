namespace EUniversity.Schedule.Manager.Contract.Exceptions;

public class EntityNotFoundException : Exception
{
    public string Entity { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }

    public EntityNotFoundException(string entity, string key, string value) : base($"'{entity}' was not found by '{key}' = '{value}'.")
    {
        Entity = entity;
        Key = key;
        Value = value;
    }

    public EntityNotFoundException(string message) : base(message) { }
}
