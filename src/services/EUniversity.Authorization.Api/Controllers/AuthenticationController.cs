using EUniversity.Authorization.Api.Commands;
using EUniversity.Authorization.Contract.Requests;
using EUniversity.Authorization.Contract.Response;
using EUniversity.Shared.Constants;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Authorization.Api.Controllers;

[ApiController]
[Route("api/authenticate")]
[Authorize(AuthenticationSchemes = SharedApiKeyContants.SchemeName)]
public class AuthenticationController(ILogger<AuthenticationController> logger, IMediator mediator) : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger = logger.ThrowIfNull();
    private readonly IMediator _mediator = mediator.ThrowIfNull();

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
