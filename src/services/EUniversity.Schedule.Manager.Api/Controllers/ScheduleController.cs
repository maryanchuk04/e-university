using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Manager.Api.Controllers;

[ApiController]
[Route("api/schedule")]
public class ScheduleController : ControllerBase
{
    public ScheduleController()
    {
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadAsync()
    {

        await Task.CompletedTask;
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateFromJsonAsync(CancellationToken token)
    {
        await Task.CompletedTask;
        return Ok();
    }
}
