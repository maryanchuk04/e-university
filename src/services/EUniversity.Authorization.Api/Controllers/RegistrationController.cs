using System.ComponentModel.DataAnnotations;
using EUniversity.Authorization.Api.Commands;
using EUniversity.Authorization.Contract.Requests;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EUniversity.Authorization.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationController(ILogger<RegistrationController> logger, IMediator mediator) : ControllerBase
{
    private readonly ILogger<RegistrationController> _logger = logger.ThrowIfNull();
    private readonly IMediator _mediator = mediator.ThrowIfNull();

    /// <summary>
    /// Registers a new user in the system and defines a role.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterRequest register, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to Register user with email = {Email}, hd = {Hd}", register.Email, register.Hd);

        if (string.IsNullOrWhiteSpace(register.Email))
        {
            return BadRequest("Email should be provided in query parameter");
        }

        await _mediator.Send(new RegisterUserCommand(register.Email, register.Hd), cancellationToken);

        return NoContent();
    }
}
