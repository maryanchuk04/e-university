using EUniversity.Schedule.Manager.Client;
using EUniversity.Schedule.Manager.Contract.Requests;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Commands.Faculty;

public class CreateFacultyTimeTableCommand(Guid facultyId, CreateTimeTableRequest timeTable) : IRequest
{
    public Guid FacultyId { get; set; } = facultyId;
    public CreateTimeTableRequest TimeTable { get; set; } = timeTable;
}

public class CreateFacultyTimeTableCommandHandler(IScheduleManagerClient scheduleManagerClient, ILogger<CreateFacultyTimeTableCommandHandler> logger) : IRequestHandler<CreateFacultyTimeTableCommand>
{
    public async Task Handle(CreateFacultyTimeTableCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await scheduleManagerClient.CreateFacultyTimeTableAsync(command.FacultyId, command.TimeTable, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured during creation timetable for Faculty = '{FacultyId}'", command.FacultyId);
            throw;
        }
    }
}
