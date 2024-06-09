using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Gateway.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimetableController(IMediator mediator) : ControllerBase
{
    //public async Task<ActionResult> GetTimetableAsync() { }

}
