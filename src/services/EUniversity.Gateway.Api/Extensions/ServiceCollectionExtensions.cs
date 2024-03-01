using System.Reflection;
using EUniversity.Gateway.Api.Swagger;
using EUniversity.Gateway.Contract;
using EUniversity.Shared.Swagger;
using Microsoft.OpenApi.Models;

namespace EUniversity.Gateway.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddGatewayServices(this IServiceCollection services, IConfiguration configuration)
    {
        //add mediatR and tell it to scan this assembly
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
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

            sa.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            sa.OperationFilter<SharedApiKeyHeaderOperationFilter>();
            var apiXmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            var contractXmlPath = Path.Combine(AppContext.BaseDirectory, $"{AssemblyName.GetAssemblyName(typeof(Class1).Assembly.Location).Name}.xml");

            sa.IncludeXmlComments(apiXmlPath, includeControllerXmlComments: true);
            sa.IncludeXmlComments(contractXmlPath);
        });
    }
}
