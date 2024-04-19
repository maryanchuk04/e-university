using System.Reflection;
using EUniversity.Authorization.Contract.Services;
using EUniversity.Shared.Authentication.Settings;
using EUniversity.Shared.Exceptions;
using EUniversity.Shared.Extensions;

namespace EUniversity.Authorization.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Add EF database context
        

        // Api key authentication for service
        services.AddPreSharedKeyAuthorization(configuration.GetSecretOrThrow<string>("ServiceApiKey"));

        services.AddScoped<ITokenGenerator, TokenGenerator>();

        return services;
    }
}
