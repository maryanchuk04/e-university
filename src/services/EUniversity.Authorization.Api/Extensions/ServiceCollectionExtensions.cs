using System.Reflection;

namespace EUniversity.Authorization.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}
