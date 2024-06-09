using EUniversity.Schedule.Manager.Client;
using EUniversity.Schedule.Manager.Contract.Requests;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Commands.Specialities;

public class CreateSpecialityCommand(CreateSpecialityRequest speciality) : IRequest<Guid>
{
    public CreateSpecialityRequest Speciality { get; set; } = speciality;
}

public class CreateSpecialityCommandHandler(IScheduleManagerClient scheduleManagerClient) : IRequestHandler<CreateSpecialityCommand, Guid>
{
    public Task<Guid> Handle(CreateSpecialityCommand command, CancellationToken cancellationToken)
    {
        return scheduleManagerClient.CreateSpecialityAsync(command.Speciality, cancellationToken);
    }
}
