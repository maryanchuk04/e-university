using EUniversity.Core.Http;
using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Contract.Requests;
using Microsoft.Extensions.Logging;

namespace EUniversity.Schedule.Manager.Client;

public class ScheduleManagerClient(
    string endpoint,
    string apiKey,
    IHttpClientFactory httpClientFactory,
    ILogger<ScheduleManagerClient> logger,
    TimeSpan? timeout = null)
    : MicroservicesClientBase<ScheduleManagerClient>(endpoint, apiKey, httpClientFactory, logger, timeout), IScheduleManagerClient
{
    private const string FacultyRoute = "/api/faculty";

    public Task<Guid> CreateFacultyAsync(CreateFacultyRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<CreateFacultyRequest, Guid>(FacultyRoute, request, cancellationToken: cancellationToken);
    }

    public async Task CreateFacultyTimeTableAsync(Guid facultyId, CreateTimeTableRequest request, CancellationToken cancellationToken = default)
    {
        var route = $"{FacultyRoute}/{facultyId}/timetable";

        await PostAsync<CreateTimeTableRequest, object>(route, request, cancellationToken: cancellationToken);
    }

    public Task<IList<FacultyDto>> GetFacultiesAsync(CancellationToken cancellationToken = default)
    {
        return GetAsync<IList<FacultyDto>>(FacultyRoute, cancellationToken: cancellationToken);
    }
}
