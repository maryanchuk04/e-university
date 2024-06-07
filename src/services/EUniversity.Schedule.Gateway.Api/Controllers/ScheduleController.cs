using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Gateway.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    /// <summary>
    /// Retrieve schedule.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    /// <summary>
    /// Retrieve schedule by Id
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetById(Guid id)
    {
        return Ok();
    }

    /// <summary>
    /// Create schedule by Xlsx file.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Create()
    {
        return Ok();
    }
}
