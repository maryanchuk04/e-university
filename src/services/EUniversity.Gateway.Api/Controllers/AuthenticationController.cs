using System.ComponentModel.DataAnnotations;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Gateway.Api.Controllers;

[ApiController]
[Route("api/authenticate")]
public class AuthenticationController(
    ILogger<AuthenticationController> logger,
    IMediator mediator) : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger = logger.ThrowIfNull();
    private readonly IMediator _mediator = mediator.ThrowIfNull();

    [HttpPost]
    public async Task<IActionResult> AuthenticateAsync([FromQuery, Required] string email, CancellationToken cancellationToken)
    {   
        _logger.LogInformation("[AuthenticationController]: Received request to authenticate user = {Email}", email);
    }
}
