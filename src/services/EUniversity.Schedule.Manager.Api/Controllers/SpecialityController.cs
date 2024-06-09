using EUniversity.Schedule.Manager.Api.Commands.Speciality;
using EUniversity.Schedule.Manager.Contract.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Manager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecialityController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateSpecialityAsync(CreateSpecialityRequest request, CancellationToken cancellationToken)
    {
        return Ok(
            await mediator.Send(new CreateSpecialityCommand(request), cancellationToken));
    }
}
