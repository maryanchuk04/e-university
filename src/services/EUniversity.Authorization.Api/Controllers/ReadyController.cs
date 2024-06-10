using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Authorization.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReadyController : ControllerBase
{
    /// <summary>
    /// Check if the service is working.
    /// </summary>
    [HttpGet]
    public string CheckReadliness()
    {
        return "e-University Authorization ready!😉🚀";
    }
}
