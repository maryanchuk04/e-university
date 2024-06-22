using EUniversity.Schedule.Gateway.Api.Commands.Groups;
using EUniversity.Schedule.Manager.Client;
using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Contract.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Gateway.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsController(IMediator mediator, ILogger<GroupsController> logger, IScheduleManagerClient scheduleManagerClient) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateGroupAsync(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(
                await mediator.Send(new CreateGroupCommand(request), cancellationToken));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured during creating group");
            throw;
        }
    }

    [HttpGet("faculty/{facultyId}")]
    public async Task<ActionResult<List<GroupInfoDto>>> GetAsync(Guid facultyId, CancellationToken cancellationToken)
    {
        return Ok(await scheduleManagerClient.GetGroupsInfoDtosAsync(facultyId, cancellationToken));
    }
}
