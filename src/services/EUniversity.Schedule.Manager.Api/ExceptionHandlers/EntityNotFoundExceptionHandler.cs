using System.Text.Json;
using EUniversity.Core.Error;
using EUniversity.Schedule.Manager.Contract.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace EUniversity.Schedule.Manager.Api.ExceptionHandlers;

public class EntityNotFoundExceptionHandler(ILogger<EntityNotFoundExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is EntityNotFoundException entityNotFoundException)
        {
            logger.LogWarning(exception, message: entityNotFoundException.Message);
            var error = new ErrorModel(System.Net.HttpStatusCode.NotFound, ($"not_found_error", entityNotFoundException.Message));

            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            return true;
        }

        return false;
    }
}
