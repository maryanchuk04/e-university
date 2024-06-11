using EUniversity.Schedule.Manager.Client;
using EUniversity.Schedule.Manager.Contract.Models;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Queries.Faculties;

public class GetFacultyTimetableQuery(Guid facultyId) : IRequest<TimeTableDto>
{
    public Guid FacultyId { get; set; } = facultyId;
}

public class GetFacultyTimetableQueryHandler(IScheduleManagerClient scheduleManagerClient) : IRequestHandler<GetFacultyTimetableQuery, TimeTableDto>
{
    public Task<TimeTableDto> Handle(GetFacultyTimetableQuery query, CancellationToken cancellationToken)
    {
        return scheduleManagerClient.GetFacultyTimeTableAsync(query.FacultyId, cancellationToken);
    }
}
