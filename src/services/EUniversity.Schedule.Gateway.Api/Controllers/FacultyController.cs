using EUniversity.Schedule.Gateway.Api.Queries.Faculties;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Gateway.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FacultyController(IMediator mediator, ILogger<FacultyController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllFacultiesAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("[FacultyController]: Get all faculties was called in Gateway");
        return Ok(await mediator.Send(new GetAllFacultiesQuery(), cancellationToken));
    }
}
