using EUniversity.Schedule.Manager.Api.Commands.Schedule;
using EUniversity.Schedule.Manager.Contract.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Manager.Api.Controllers;

[ApiController]
[Route("api/schedule")]
public class ScheduleController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateFromJsonAsync(CreateSemesterScheduleForFacultyRequest request, CancellationToken cancellationToken)
    {
        await mediator.Send(new CreateSemesterScheduleCommand(request), cancellationToken);
        return Ok();
    }
}
