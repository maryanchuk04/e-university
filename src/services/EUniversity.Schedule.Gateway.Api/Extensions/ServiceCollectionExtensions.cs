using System.Reflection;
using System.Text;
using EUniversity.Authorization.Client.Factories;
using EUniversity.Schedule.Gateway.Api.Swagger;
using EUniversity.Schedule.Gateway.Contract.Providers;
using EUniversity.Schedule.Gateway.Contract.Requests;
using EUniversity.Schedule.Manager.Client.Factories;
using EUniversity.Shared.Exceptions;
using EUniversity.Shared.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace EUniversity.Schedule.Gateway.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddGatewayServices(this IServiceCollection services, IConfiguration configuration)
    {
        // add MediatR and tell it to scan this assembly
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddHttpContextAccessor();
        services.AddScoped<IPortalUserProvider, PortalUserProvider>();
    }

    public static void AddUniversityMicroservices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddScoped<IAuthorizationClientFactory, AuthorizationClientFactory>();

        services.AddScoped(sp =>
        {
            var factory = sp.GetRequiredService<IAuthorizationClientFactory>();

            var baseAddress = configuration.GetSecretOrThrow<string>("MicroserviceBaseAddress:Authorization");
            var apiKey = configuration.GetSecretOrThrow<string>("ApiKeys:Authorization");

            return factory.Create(baseAddress, apiKey);
        });

        services.AddScoped<IScheduleManagerClientFactory, ScheduleManagerClientFactory>();

        services.AddScoped(sp =>
        {
            var factory = sp.GetRequiredService<IScheduleManagerClientFactory>();

            var baseAddress = configuration.GetSecretOrThrow<string>("MicroserviceBaseAddress:Manager");
            var apiKey = configuration.GetSecretOrThrow<string>("ApiKeys:Manager");

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

    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtSettings:Key"));
        var issuer = configuration.GetValue<string>("JwtSettings:Issuer");
        var audience = configuration.GetValue<string>("JwtSettings:Audience");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }
}
