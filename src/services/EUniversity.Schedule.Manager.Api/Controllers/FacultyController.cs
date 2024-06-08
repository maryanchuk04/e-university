using System.Net;
using EUniversity.Core.Error;
using EUniversity.Schedule.Manager.Api.Commands.Faculty;
using EUniversity.Schedule.Manager.Api.Error;
using EUniversity.Schedule.Manager.Api.Queries;
using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Manager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FacultyController(IMediator mediator, ILogger<FacultyController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator.ThrowIfNull();
    private readonly ILogger<FacultyController> _logger = logger.ThrowIfNull();

    [HttpGet]
    public async Task<ActionResult<IList<FacultyDto>>> GetFacultiesAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get faculties was executed");
        return Ok(await _mediator.Send(new GetFacultiesQuery(), cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateFacultyAsync([FromBody] CreateFacultyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            request.ThrowIfNull();

            var facultyId = await _mediator.Send(new CreateFacultyCommand(request), cancellationToken);
            return Ok(facultyId);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogWarning(ex, "Create faculty request body was null");
            return BadRequest(new ErrorModel(HttpStatusCode.BadRequest, ApiErrorCodes.InvalidRequest));
        }
    }

    [HttpPost]
    [Route("{facultyId:guid}/timetable")]
    public async Task<IActionResult> CreateFacultyTimeTableAsync(Guid facultyId, [FromBody] CreateTimeTableRequest request, CancellationToken cancellationToken)
    {
        try
        {
            request.ThrowIfNull();
            facultyId.ThrowIfNullOrDefault();

            await _mediator.Send(new CreateFacultyTimeTableCommand(facultyId, request), cancellationToken);

            return Ok();
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogWarning(ex, "Create timetable request body was null");
            return BadRequest(new ErrorModel(HttpStatusCode.BadRequest, ApiErrorCodes.InvalidRequest));
        }
    }
}
