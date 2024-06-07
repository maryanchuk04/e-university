using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Gateway.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    /// <summary>
    /// Retrieve Users.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    /// <summary>
    /// Retrieve User by Id
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetByid(Guid Id)
    {
        return Ok();
    }

    /// <summary>
    /// Retrieve Current user by access token.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetCurrentUser()
    {
        return Ok();
    }
}
