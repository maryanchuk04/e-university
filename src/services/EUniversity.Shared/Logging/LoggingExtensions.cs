namespace EUniversity.Shared.Logging;

public static class LoggingExtensions
{
    public static Dictionary<string, object> ToLoggingContextProperties(this object obj)
    {
        var properties = new Dictionary<string, object>();
        var type = obj.GetType();
        var propertiesInfo = type.GetProperties();
        foreach (var propertyInfo in propertiesInfo)
        {
            var value = propertyInfo.GetValue(obj);
            if (value != null)
            {
                // camel case the property name!
                var fixedName = System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(propertyInfo.Name);
                properties.Add(fixedName, value);
            }
        }

        return properties;
    }
}
