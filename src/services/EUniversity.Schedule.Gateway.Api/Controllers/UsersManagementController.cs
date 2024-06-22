using EUniversity.Schedule.Gateway.Api.Commands;
using EUniversity.Schedule.Gateway.Api.Commands.Students;
using EUniversity.Schedule.Gateway.Api.Commands.Teacher;
using EUniversity.Schedule.Gateway.Api.Queries.Users.Students;
using EUniversity.Schedule.Gateway.Api.Queries.Users.Teachers;
using EUniversity.Schedule.Gateway.Contract.Models;
using EUniversity.Schedule.Gateway.Contract.Requests;
using EUniversity.Schedule.Gateway.Contract.Responses;
using EUniversity.Schedule.Manager.Client;
using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CreateStudentRequest = EUniversity.Schedule.Gateway.Contract.Requests.CreateStudentRequest;

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
    public async Task<ActionResult<List<TeacherResponse>>> GetTeachersAsync(Guid facultyId, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetTeachersQuery(facultyId),  cancellationToken));
    }

    [HttpPost("student")]
    public async Task<ActionResult<Guid>> CreateStudentAsync(Guid facultyId, CreateStudentRequest request, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new CreateStudentCommand(request), cancellationToken));
    }

    [HttpPost("teacher")]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateTeacherAsync(Guid facultyId, CreateTeacherRequest request, CancellationToken cancellationToken)
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

    [HttpPost("delete/users")]
    public async Task<ActionResult> DeleteUserAsync(DeleteUsersRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await mediator.Send(new DeleteUserCommand(request.UserIds), cancellationToken);
            return NoContent();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
