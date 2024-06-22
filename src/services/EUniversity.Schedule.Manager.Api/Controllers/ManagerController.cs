using EUniversity.Schedule.Manager.Api.Commands.Manager;
using EUniversity.Schedule.Manager.Api.Queries.Manager;
using EUniversity.Schedule.Manager.Contract.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Manager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManagerController(IMediator mediator) : ControllerBase
{
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<ManagerDto>> GetAsync(Guid userId, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetManagerInfoQuery(userId), cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateManagerAsync(ManagerDto managerDto, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new CreateManagerCommand(managerDto), cancellationToken));
    }
}
