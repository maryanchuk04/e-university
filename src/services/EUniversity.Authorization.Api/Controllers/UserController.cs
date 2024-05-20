using EUniversity.Authorization.Api.Queries.Users;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Authorization.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator.ThrowIfNull();

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetUserQuery(userId), cancellationToken));
    }

    [HttpGet]
    public async Task<ActionResult> GetUserByEmailAsync([FromQuery] string email, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetUserQuery(email), cancellationToken));
    }
}
