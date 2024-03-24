using EUniversity.Authorization.Api.Queries;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Authorization.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator.ThrowIfNull();

    [HttpGet]
    public async Task<ActionResult<bool>> CheckIfUserExistInSystemAsync([FromQuery] string email, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new CheckIfUserExistQuery(email), cancellationToken));
    }
}
