using System.Net;
using EUniversity.Core.Error;
using EUniversity.Schedule.Manager.Api.Commands.Teacher;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Shared.ErrorHandling;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Manager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController(IMediator mediator, ILogger<TeacherController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator.ThrowIfNull();
    private readonly ILogger<TeacherController> _logger = logger.ThrowIfNull();

    [HttpPost]
    public async Task<IActionResult> CreateTeacherAsync([FromBody] CreateTeacherRequest request, CancellationToken cancellationToken)
    {

        try
        {
            request.ThrowIfNull();

            var teacherId = await _mediator.Send(new CreateTeacherCommand(request), cancellationToken);
            return Ok(teacherId);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogWarning(ex, "Create teacher request body was null");
            return BadRequest(new ErrorModel(HttpStatusCode.BadRequest, ApiErrorCodes.InvalidRequest));
        }
    }
}
