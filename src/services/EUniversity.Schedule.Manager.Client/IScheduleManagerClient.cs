using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Contract.Requests;

namespace EUniversity.Schedule.Manager.Client;

public interface IScheduleManagerClient
{
    Task<IList<FacultyDto>> GetFacultiesAsync(CancellationToken cancellationToken = default);
    
    Task<Guid> CreateFacultyAsync(CreateFacultyRequest request, CancellationToken cancellationToken = default);
    
    Task CreateFacultyTimeTableAsync(Guid facultyId, CreateTimeTableRequest request, CancellationToken cancellationToken = default);
}
