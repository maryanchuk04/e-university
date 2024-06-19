using EUniversity.Schedule.Manager.Api.Queries.Students;
using EUniversity.Schedule.Manager.Contract.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Manager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController(IMediator mediator) : ControllerBase
{
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<StudentInfoDto>> GetStudentInfoByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return Ok(
            await mediator.Send(new GetStudentInfoQuery(userId), cancellationToken));
    }

    [HttpGet("faculty/{facultyId:guid}")]
    public async Task<ActionResult<List<StudentInfoDto>>> GetStudentsByFacultyIdAsync(Guid facultyId, CancellationToken cancellationToken)
    {
        return Ok(
            await mediator.Send(new GetStudentsByFacultyQuery(facultyId), cancellationToken));
    }

}
