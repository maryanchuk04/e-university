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
        return await scheduleManagerClient.GetManagerInfoByUserIdAsync(request.UserId, cancellationToken);
    }
}
