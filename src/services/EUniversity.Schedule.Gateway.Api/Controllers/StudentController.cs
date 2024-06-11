using EUniversity.Schedule.Gateway.Api.Queries.Users.Students;
using EUniversity.Schedule.Gateway.Contract.Responses;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Gateway.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentController(IMediator mediator, ILogger<StudentController> logger) : ControllerBase
{
    private readonly ILogger<StudentController> _logger = logger.ThrowIfNull();
    private readonly IMediator _mediator = mediator.ThrowIfNull();

    [HttpGet]
    public async Task<IActionResult> GetCurrentStudentInformationAsync(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetCurrentStudentQuery(), cancellationToken));
    }

    /// <summary>
    /// Get current student Day schedule.
    /// </summary>
    /// <returns>MyDayGatewayView gateway view</returns>
    [HttpGet]
    [Route("my-day")]
    public async Task<ActionResult<MyDayGatewayView>> GetStudentDayAsync(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetStudentDayQuery(), cancellationToken);
    }
}
