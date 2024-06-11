using EUniversity.Schedule.Gateway.Api.Queries.Schedule;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Gateway.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("faculty/{facultyId}")]
    public async Task<IActionResult> GetScheduleAsync(Guid facultyId, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetScheduleQuery(facultyId), cancellationToken);

        return Ok(response);
    }
}
