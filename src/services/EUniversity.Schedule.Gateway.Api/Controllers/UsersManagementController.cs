using EUniversity.Schedule.Gateway.Api.Commands.Teacher;
using EUniversity.Schedule.Gateway.Api.Queries.Users.Students;
using EUniversity.Schedule.Gateway.Contract.Models;
using EUniversity.Schedule.Manager.Client;
using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Gateway.Api.Controllers;

[ApiController]
[Route("api/users-management/faculty/{facultyId}")]
public class UsersManagementController(IMediator mediator, IScheduleManagerClient scheduleManagerClient) : ControllerBase
{
    [HttpGet("students")]
    public async Task<ActionResult<List<StudentGatewayView>>> GetStudentsAsync(Guid facultyId, CancellationToken cancellationToken)
    {
        return Ok(
            await mediator.Send(new GetStudentsQuery(facultyId), cancellationToken));
    }

    [HttpGet("teachers")]
    public async Task<ActionResult<List<TeacherDto>>> GetTeachersAsync(Guid facultyId, CancellationToken cancellationToken)
    {
        return Ok(await scheduleManagerClient.GetTeachersByFacultyIdAsync(facultyId, cancellationToken));
    }

    [HttpPost("student")]
    public async Task<IActionResult> CreateStudentAsync()
    {
        await Task.CompletedTask;
        return Ok();
    }

    [HttpPost("teacher")]
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


    //[HttpPost]
    //public async Task<ActionResult> CreateStudentAsync(CancellationToken cancellationToken)
    //{

    //}
}
