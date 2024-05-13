using System.Reflection;
using EUniversity.Authorization.Api.Swagger;
using EUniversity.Authorization.Contract.Requests;
using EUniversity.Shared.Swagger;
using Microsoft.OpenApi.Models;

namespace EUniversity.Authorization.Api.Extensions;

public static class SwaggerExtensions
{
    public const string APIDocumentationPath = $"/swagger/v1/swagger.json";

    public static void AddAuthenticationServiceSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(sa =>
        {
            sa.SwaggerDoc(AuthSwaggerContants.APIVersion, new OpenApiInfo
            {
                Title = AuthSwaggerContants.APITitle,
                Description = AuthSwaggerContants.APIDescription,
                Version = AuthSwaggerContants.APIVersion,
            });

            //sa.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            sa.OperationFilter<SharedApiKeyHeaderOperationFilter>();
            var apiXmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            var contractXmlPath = Path.Combine(AppContext.BaseDirectory, $"{AssemblyName.GetAssemblyName(typeof(AuthenticateRequest).Assembly.Location).Name}.xml");

            sa.IncludeXmlComments(apiXmlPath, includeControllerXmlComments: true);
            sa.IncludeXmlComments(contractXmlPath);
        });
    }

    public static void UseAuthorizationSwaggerUI(this IApplicationBuilder app)
    {
        app.UseSwaggerUI(x => x.SwaggerEndpoint(APIDocumentationPath, AuthSwaggerContants.APITitle));
    }
}
