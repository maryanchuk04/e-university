using EUniversity.Schedule.Gateway.Api.Commands.Specialities;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Gateway.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecialityController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateSpecialityAsync(CreateSpecialityRequest request, CancellationToken cancellationToken)
    {
        try
        {
            request.ThrowIfNull();

            return Ok(
                await mediator.Send(new CreateSpecialityCommand(request), cancellationToken));
        }
        catch (Exception)
        {

            throw;
        }
    }
}
