using Microsoft.Extensions.DependencyInjection;

namespace EUniversity.Authorization.Contract.Extensions;

public static class AuthorizationClientExtensions
{
    public static IServiceCollection AddAuthorizationClient(this IServiceCollection services)
    {
        return services;
    }
}
