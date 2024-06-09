using EUniversity.Schedule.Gateway.Api.Commands.Teacher;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Gateway.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController(IMediator mediator, ILogger<TeacherController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateTeacherAsync(CreateTeacherRequest request, CancellationToken cancellationToken)
    {
        try
        {
            request.ThrowIfNull();

            return Ok(await mediator.Send(new CreateTeacherCommand(request), cancellationToken));
        }
        catch (Exception)
        {
            throw;
        }
    }
}
