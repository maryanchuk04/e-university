using EUniversity.Schedule.Gateway.Api.Commands.Faculty;
using EUniversity.Schedule.Gateway.Api.Queries.Faculties;
using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Shared.Extensions;
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

    [HttpGet]
    [Route("{facultyId}/timetable")]
    public async Task<ActionResult<TimeTableDto>> GetFacultyTimetableAsync(Guid facultyId, CancellationToken cancellationToken)
    {
        logger.LogInformation("[FacultyController]: Get faculty timetable for Faculty = {FacultyId} was called in Gateway", facultyId);
        return Ok(await mediator.Send(new GetFacultyTimetableQuery(facultyId), cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateFacultyAsync(CreateFacultyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            request.ThrowIfNull();

            logger.LogInformation("[FacultyController]: Create new faculty was called");
            return Ok(await mediator.Send(new CreateFacultyCommand(request), cancellationToken));
        }
        catch (Exception)
        {
            // TODO: Add error handling.
            throw;
        }
    }

    [HttpPost]
    [Route("{facultyId:guid}/timetable")]
    public async Task<ActionResult> CreateFacultyAsync(Guid facultyId, CreateTimeTableRequest request, CancellationToken cancellationToken)
    {
        try
        {
            request.ThrowIfNull();

            logger.LogInformation("[FacultyController]: Create new timetable for FacultyId = '{FacultyId}' was called", facultyId);
            await mediator.Send(new CreateFacultyTimeTableCommand(facultyId, request), cancellationToken);

            return Ok();
        }
        catch (Exception)
        {
            // TODO: Add error handling.
            throw;
        }
    }
}
