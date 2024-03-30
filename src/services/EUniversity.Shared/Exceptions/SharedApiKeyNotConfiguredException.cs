namespace BizPlanner.Authorization.Exceptions;

public class SharedApiKeyNotConfiguredException(string service)
    : Exception(message: $"Shared Api key not configured for service = {service}")
{
}
