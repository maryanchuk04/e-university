using EUniversity.Schedule.Manager.Client;
using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Shared.Extensions;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Queries.Faculties;

public class GetAllFacultiesQuery : IRequest<IList<FacultyDto>> { }

public class GetAllFacultiesQueryHandler(IScheduleManagerClient scheduleManagerClient) : IRequestHandler<GetAllFacultiesQuery, IList<FacultyDto>>
{
    private readonly IScheduleManagerClient _scheduleManagerClient = scheduleManagerClient.ThrowIfNull();

    public Task<IList<FacultyDto>> Handle(GetAllFacultiesQuery request, CancellationToken cancellationToken)
    {
        return _scheduleManagerClient.GetFacultiesAsync(cancellationToken);
    }
}
