using EUniversity.Authorization.Client;
using EUniversity.Schedule.Manager.Client;
using EUniversity.Schedule.Manager.Contract.Models;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Queries.Users.Manager;

public class GetManagerQuery(Guid userId) : IRequest<ManagerDto>
{
    public Guid UserId { get; set; } = userId;
}

public class GetManagerQueryHandler(IScheduleManagerClient scheduleManagerClient, IAuthorizationClient authorizationClient) : IRequestHandler<GetManagerQuery, ManagerDto>
{
    public async Task<ManagerDto> Handle(GetManagerQuery request, CancellationToken cancellationToken)
    {
        var managerTask = scheduleManagerClient.GetManagerInfoByUserIdAsync(request.UserId, cancellationToken);

        var userTask = authorizationClient.GetUserAsync(request.UserId, cancellationToken);

        var manager = await managerTask;
        var user = await userTask;

        manager.Picture = user.Picture;
        manager.FullName = user.FullName;

        return manager;
    }
}
