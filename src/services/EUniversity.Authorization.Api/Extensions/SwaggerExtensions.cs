using System.Reflection;
using EUniversity.Authorization.Api.Swagger;
using EUniversity.Shared.Swagger;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.OpenApi.Models;

namespace EUniversity.Authorization.Api.Extensions;

public static class SwaggerExtensions
{
    public static void AddAuthenticationServiceSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(sa =>
        {
            sa.SwaggerDoc(AuthSwaggerContants.APITitle, new OpenApiInfo
            {
                Title = AuthSwaggerContants.APITitle,
                Version = AuthSwaggerContants.APIVersion,
                Description = AuthSwaggerContants.APIDescription,
            });

            sa.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            sa.OperationFilter<SharedApiKeyHeaderOperationFilter>();
            var apiXmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            var contractXmlPath = Path.Combine(AppContext.BaseDirectory, $"{AssemblyName.GetAssemblyName(typeof(RegisterRequest).Assembly.Location).Name}.xml");

            sa.IncludeXmlComments(apiXmlPath, includeControllerXmlComments: true);
            sa.IncludeXmlComments(contractXmlPath);
        });
    }
}
