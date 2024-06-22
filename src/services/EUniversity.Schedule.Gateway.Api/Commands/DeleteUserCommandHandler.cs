using EUniversity.Authorization.Client;
using EUniversity.Schedule.Manager.Client;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Commands;

public class DeleteUserCommand(List<Guid> userIds) : IRequest<Unit>
{
    public List<Guid> UserIds { get; set; } = userIds;
}

public class DeleteUserCommandHandler(IScheduleManagerClient scheduleManagerClient, IAuthorizationClient authorizationClient) : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var tasks = request.UserIds.Select(userId => DeleteUserAsync(userId, cancellationToken)).ToList();
        await Task.WhenAll(tasks);

        return Unit.Value;
    }

    private async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        await Task.WhenAll(
            scheduleManagerClient.DeleteUserAsync(userId, cancellationToken),
            authorizationClient.DeleteUserAsync(userId, cancellationToken));
    }
}
