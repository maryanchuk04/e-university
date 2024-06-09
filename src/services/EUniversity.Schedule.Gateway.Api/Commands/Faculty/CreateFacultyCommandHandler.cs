using EUniversity.Schedule.Manager.Client;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Shared.Extensions;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Commands.Faculty;

public class CreateFacultyCommand(CreateFacultyRequest faculty) : IRequest<Guid>
{
    public CreateFacultyRequest Faculty { get; set; } = faculty;
}


public class CreateFacultyCommandHandler(IScheduleManagerClient scheduleManagerClient, ILogger<CreateFacultyCommandHandler> logger) : IRequestHandler<CreateFacultyCommand, Guid>
{
    private readonly IScheduleManagerClient _scheduleManagerClient = scheduleManagerClient.ThrowIfNull();
    private readonly ILogger<CreateFacultyCommandHandler> _logger = logger.ThrowIfNull();

    public async Task<Guid> Handle(CreateFacultyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await _scheduleManagerClient.CreateFacultyAsync(request.Faculty, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while creating faculty");
            throw;
        }
    }
}
