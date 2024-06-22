using Azure.Core;
using EUniversity.Schedule.Manager.Api.Commands.Groups;
using EUniversity.Schedule.Manager.Api.Queries.Groups;
using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Manager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsController(IMediator mediator, ILogger<GroupsController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateGroupAsync(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        try
        {
            request.ThrowIfNull();

            return Ok(
                await mediator.Send(new CreateGroupCommand(request), cancellationToken));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured during creation Group with name = {GroupName}", request.Name);
            throw;
        }
    }

    [HttpGet("faculty/{facultyId}")]
    public async Task<ActionResult<List<GroupInfoDto>>> GetGroupInfosAsync(Guid facultyId, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(
                await mediator.Send(new GetGroupsInfoQuery(facultyId), cancellationToken));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured during retrieving groups");
            throw;
        }
    }
}
