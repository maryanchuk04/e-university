using EUniversity.Schedule.Manager.Client;
using EUniversity.Schedule.Manager.Contract.Responses;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Queries.Schedule;

public class GetScheduleQuery(Guid facultyId) : IRequest<ScheduleResponse>
{
    public Guid FacultyId { get; set; } = facultyId;
}

public class GetScheduleQueryHandler(IScheduleManagerClient scheduleManagerClient) : IRequestHandler<GetScheduleQuery, ScheduleResponse>
{
    public Task<ScheduleResponse> Handle(GetScheduleQuery query, CancellationToken cancellationToken)
    {
        return scheduleManagerClient.GetScheduleAsync(query.FacultyId, cancellationToken);
    }
}
