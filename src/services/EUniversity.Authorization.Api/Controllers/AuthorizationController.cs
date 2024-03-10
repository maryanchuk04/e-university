using EUniversity.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Authorization.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController(ILogger<AuthorizationController> logger) : ControllerBase
{
    private readonly ILogger<AuthorizationController> _logger = logger.ThrowIfNull();

    [HttpPost]
    public async Task<IActionResult> AuthenticateAsync()
    {
        await Task.CompletedTask;
        return NoContent();
    }
}
