using EUniversity.Gateway.Api.Commands;
using EUniversity.Gateway.Contract.Requests;
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

    /// <summary>
    /// Authenticate user endpoint
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[AuthenticationController]: Received request to authenticate user = {Email}", request.Email);
        var res = await _mediator.Send(new AuthenticateUserCommand(request), cancellationToken);
        return Ok(res);
    }
}
