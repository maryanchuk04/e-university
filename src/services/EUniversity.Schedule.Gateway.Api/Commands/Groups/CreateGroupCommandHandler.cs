using EUniversity.Schedule.Manager.Client;
using EUniversity.Schedule.Manager.Contract.Requests;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Commands.Groups;

public class CreateGroupCommand(CreateGroupRequest group) : IRequest<Guid>
{
    public CreateGroupRequest Group { get; set; } = group;
}

public class CreateGroupCommandHandler(IScheduleManagerClient scheduleManagerClient) : IRequestHandler<CreateGroupCommand, Guid>
{
    public Task<Guid> Handle(CreateGroupCommand command, CancellationToken cancellationToken)
    {
        return scheduleManagerClient.CreateGroupAsync(command.Group, cancellationToken);
    }
}
