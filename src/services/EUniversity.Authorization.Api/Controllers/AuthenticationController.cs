using EUniversity.Authorization.Api.Commands;
using EUniversity.Authorization.Contract.Requests;
using EUniversity.Authorization.Contract.Response;
using EUniversity.Shared.Constants;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Authorization.Api.Controllers;

/// <summary>
/// Authenticate endpoints
/// </summary>
[ApiController]
[Route("api/authenticate")]
[Authorize(AuthenticationSchemes = SharedApiKeyContants.SchemeName)]
public class AuthenticationController(ILogger<AuthenticationController> logger, IMediator mediator) : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger = logger.ThrowIfNull();
    private readonly IMediator _mediator = mediator.ThrowIfNull();

    /// <summary>
    /// Authenticate user.
    /// </summary>
    /// <param name="request">Auth request</param>
    /// <returns>User Id and Tokens</returns>
    [HttpPost]
    public async Task<ActionResult<AuthenticateResponse>> AuthenticateAsync([FromBody] AuthenticateRequest request)
    {
        if (string.IsNullOrEmpty(request.Email))
        {
            return BadRequest("Email field should be provided");
        }

        return Ok(await _mediator.Send(new AuthenticateUserCommand(request)));
    }
}
