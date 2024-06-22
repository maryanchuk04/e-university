using EUniversity.Schedule.Manager.Api.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Manager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpDelete("{userId}")]
    public async Task<ActionResult> DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteUserCommand(userId), cancellationToken);

        return NoContent();
    }

}
