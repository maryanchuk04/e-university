using System.Text.Json;
using System.Text.Json.Serialization;
using EUniversity.Core.Error;
using EUniversity.Schedule.Manager.Api.Error;
using Microsoft.AspNetCore.Diagnostics;

namespace EUniversity.Schedule.Manager.Api.ExceptionHandlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    const string contentType = "application/problem+json";

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled exception has occurred while executing the request.");

        context.Response.ContentType = contentType;

        var error = new ErrorModel(System.Net.HttpStatusCode.InternalServerError, ApiErrorCodes.InternalServerError);
        await context.Response.WriteAsync(JsonSerializer.Serialize(error, SerializerOptions), cancellationToken);

        return true;
    }
}
