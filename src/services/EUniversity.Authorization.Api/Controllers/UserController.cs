﻿using EUniversity.Authorization.Api.Queries.Users;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Authorization.Api.Controllers;

/// <summary>
/// User endpoints
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator.ThrowIfNull();

    /// <summary>
    /// Get user info by UserId.
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetUserQuery(userId), cancellationToken));
    }

    /// <summary>
    /// Get user info by Email.
    /// </summary>
    /// <param name="email">User email</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> GetUserByEmailAsync([FromQuery] string email, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetUserQuery(email), cancellationToken));
    }
}
