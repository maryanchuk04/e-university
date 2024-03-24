using Microsoft.Extensions.Configuration;

namespace EUniversity.Shared.Exceptions;

public class MissingRequiredConfigurationException(string secretName)
    : Exception($"Missing required configuration key = {secretName}.")
{
}

public static class MissingConfigurationExtensions
{
    public static T GetSecretOrThrow<T>(this IConfiguration configuration, string secretName)
    {
        return configuration.GetValue<T>(secretName)
            ?? throw new MissingRequiredConfigurationException(secretName);
    }
}