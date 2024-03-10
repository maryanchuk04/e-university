using EUniversity.Shared.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EUniversity.Authorization.Api.Filters;

public class GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger) : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger = logger.ThrowIfNull();

    public void OnException(ExceptionContext context)
    {
        throw new NotImplementedException();
    }
}
