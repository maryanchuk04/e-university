using System.Reflection;
using EUniversity.Authorization.Client.Factories;
using EUniversity.Gateway.Api.Swagger;
using EUniversity.Gateway.Contract.Requests;
using EUniversity.Shared.Exceptions;
using EUniversity.Shared.Swagger;
using Microsoft.OpenApi.Models;

namespace EUniversity.Gateway.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddGatewayServices(this IServiceCollection services, IConfiguration configuration)
    {
        // add MediatR and tell it to scan this assembly
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    public static void AddUniversityMicroservices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthorizationClientFactory, AuthorizationClientFactory>();

        services.AddScoped(sp =>
        {
            var factory = sp.GetRequiredService<IAuthorizationClientFactory>();

            var baseAddress = configuration.GetSecretOrThrow<string>("MicroserviceBaseAddress:Authorization");
            var apiKey = configuration.GetSecretOrThrow<string>("ApiKeys:Authorization");

            return factory.Create(baseAddress, apiKey);
        });
    }

    public static void AddGatewaySwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(sa =>
        {
            sa.SwaggerDoc(GatewaySwaggerConstants.APITitle, new OpenApiInfo
            {
                Title = GatewaySwaggerConstants.APITitle,
                Version = GatewaySwaggerConstants.APIVersion,
                Description = GatewaySwaggerConstants.APIDescription,
            });
            sa.OperationFilter<SharedApiKeyHeaderOperationFilter>();
            var apiXmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            var contractXmlPath = Path.Combine(AppContext.BaseDirectory, $"{AssemblyName.GetAssemblyName(typeof(AuthenticateRequest).Assembly.Location).Name}.xml");

            sa.IncludeXmlComments(apiXmlPath, includeControllerXmlComments: true);
            sa.IncludeXmlComments(contractXmlPath);
        });
    }
}
