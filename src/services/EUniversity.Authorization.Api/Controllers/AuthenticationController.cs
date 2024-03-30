using EUniversity.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Authorization.Api.Controllers;

[ApiController]
[Route("api/authenticate")]
[Authorize]
public class AuthenticationController(ILogger<AuthenticationController> logger) : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger = logger.ThrowIfNull();

    [HttpPost]
    public async Task<IActionResult> AuthenticateAsync()
    {
        _logger.LogInformation("Authorization worked!");
        await Task.CompletedTask;
        return NoContent();
    }
}
