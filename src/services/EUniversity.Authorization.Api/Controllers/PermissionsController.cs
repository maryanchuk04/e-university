using EUniversity.Authorization.Api.Commands;
using EUniversity.Authorization.Contract.Requests;
using EUniversity.Shared.Constants;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Authorization.Api.Controllers;

/// <summary>
/// Permissions endpoints
/// </summary>
[ApiController]
[Route("api/permissions")]
[Authorize(AuthenticationSchemes = SharedApiKeyContants.SchemeName)]
public class PermissionsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator.ThrowIfNull();

    /// <summary>
    /// Assign permissions to a user.
    /// </summary>
    /// <param name="userId">User id.</param>
    /// <param name="request">List of permissions</param>
    [HttpPost]
    [Route("user/{userId}")]
    public async Task<IActionResult> AssignPermissionsToUserAsync(Guid userId, AssignPermissionsToUserRequest request)
    {
        if (request.Permissions.Length == 0)
            return NoContent();

        await _mediator.Send(new AssignPermissionsToUserCommand(userId, request.Permissions));
        return Ok();
    }
}
