﻿using EUniversity.Core.Error;
using EUniversity.Schedule.Manager.Api.Error;
using System.Net;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EUniversity.Schedule.Manager.Api.Commands.Faculty;
using EUniversity.Schedule.Manager.Api.Commands.TimeTable;

namespace EUniversity.Schedule.Manager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FacultyController(IMediator mediator, ILogger<FacultyController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator.ThrowIfNull();
    private readonly ILogger<FacultyController> _logger = logger.ThrowIfNull();

    [HttpPost]
    public async Task<IActionResult> CreateFacultyAsync([FromBody] CreateFacultyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            request.ThrowIfNull();

            await _mediator.Send(new CreateFacultyCommand(request), cancellationToken);
            return Ok();
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogWarning(ex, "Create faculty request body was null");
            return BadRequest(new ErrorModel(HttpStatusCode.BadRequest, ApiErrorCodes.InvalidRequest));
        }
    }

    [HttpPost]
    [Route("{facultyId:guid}/timetable")]
    public async Task<IActionResult> CreateFacultyTimeTable(Guid facultyId, [FromBody] CreateTimeTableRequest request, CancellationToken cancellationToken)
    {
        try
        {
            request.ThrowIfNull();
            facultyId.ThrowIfNullOrDefault();

            await _mediator.Send(new CreateTimeTableCommand(facultyId, request), cancellationToken);

            return Ok();
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogWarning(ex, "Create timetable request body was null");
            return BadRequest(new ErrorModel(HttpStatusCode.BadRequest, ApiErrorCodes.InvalidRequest));
        }
    }
}
