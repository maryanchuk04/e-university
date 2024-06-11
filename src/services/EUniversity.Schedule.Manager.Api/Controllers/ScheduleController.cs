using EUniversity.Schedule.Manager.Api.Commands.Schedule;
using EUniversity.Schedule.Manager.Api.Queries.Schedule;
using EUniversity.Schedule.Manager.Contract.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Manager.Api.Controllers;

[ApiController]
[Route("api/schedule")]
public class ScheduleController(IMediator mediator) : ControllerBase
{
    [HttpGet("faculty/{facultyId:guid}")]
    public async Task<IActionResult> GetSemesterScheduleAsync(Guid facultyId, CancellationToken cancellationToken)
    {
        var query = new GetSemesterScheduleQuery(facultyId);
        var response = await mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFromJsonAsync(CreateSemesterScheduleForFacultyRequest request, CancellationToken cancellationToken)
    {
        await mediator.Send(new CreateSemesterScheduleCommand(request), cancellationToken);
        return Ok();
    }
}
