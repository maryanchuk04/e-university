using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Schedule.Manager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReadyController : ControllerBase
{
    [HttpGet]
    public string CheckReadliness()
    {
        return "e-University Manager ready!😉🚀";
    }
}
