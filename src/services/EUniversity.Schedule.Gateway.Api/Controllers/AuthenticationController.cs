﻿using EUniversity.Schedule.Gateway.Api.Commands.Authentication;
using EUniversity.Schedule.Gateway.Api.Extensions;
using EUniversity.Schedule.Gateway.Contract.Requests;
using EUniversity.Shared.Exceptions;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Gateway.Api.Controllers;

[ApiController]
[Route("api/authenticate")]
public class AuthenticationController(
    ILogger<AuthenticationController> logger,
    IMediator mediator, IConfiguration configuration) : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger = logger.ThrowIfNull();
    private readonly IMediator _mediator = mediator.ThrowIfNull();
    private readonly IConfiguration _configuration = configuration.ThrowIfNull();

    /// <summary>
    /// Authenticate user endpoint
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("[AuthenticationController]: Received request to authenticate user = {Email}", request.Email);
            var res = await _mediator.Send(new AuthenticateUserCommand(request), cancellationToken);

            HttpContext.SetAuthCookies(res, _configuration.GetSecretOrThrow<string>("CookiesDomain"));

            return Ok();
        }
        catch (Exception)
        {
            return Unauthorized();
        }
    }

    [HttpPost("refresh-access-token")]
    public async Task<IActionResult> RefreshAccessToken([FromQuery] string refreshToken, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
            return BadRequest("Refresh token should be provided.");

        try
        {
            var res = await _mediator.Send(new RefreshAccessTokenCommand(refreshToken), cancellationToken);

            HttpContext.SetAuthCookies(res, _configuration.GetSecretOrThrow<string>("CookiesDomain"));
            return Ok();
        }
        catch (Exception)
        {
            return Unauthorized();
        }
    }
}
