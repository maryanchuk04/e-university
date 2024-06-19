using EUniversity.Schedule.Gateway.Api.Queries.Users.Manager;
using EUniversity.Schedule.Gateway.Contract.Providers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Gateway.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManagerController(IMediator mediator, IPortalUserProvider portalUserProvider) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCurrentAdminAsync(CancellationToken cancellationToken)
    {
        var user = portalUserProvider.GetPortalUser();

        if (!user.IsAdmin())
        {
            return Forbid();
        }

        return Ok(
            await mediator.Send(new GetManagerQuery(user.UserId), cancellationToken));
    }
}
