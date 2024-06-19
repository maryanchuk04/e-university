using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Schedule.Manager.Contract.Responses;

namespace EUniversity.Schedule.Manager.Client;

public interface IScheduleManagerClient
{
    Task<IList<FacultyDto>> GetFacultiesAsync(CancellationToken cancellationToken = default);
    
    Task<Guid> CreateFacultyAsync(CreateFacultyRequest request, CancellationToken cancellationToken = default);
    
    Task CreateFacultyTimeTableAsync(Guid facultyId, CreateTimeTableRequest request, CancellationToken cancellationToken = default);

    Task<Guid> CreateTeacherAsync(CreateTeacherRequest request, CancellationToken cancellationToken = default);

    Task<StudentInfoDto> GetStudentInfoByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Guid> CreateGroupAsync(CreateGroupRequest request, CancellationToken cancellationToken = default);

    Task<Guid> CreateSpecialityAsync(CreateSpecialityRequest request, CancellationToken cancellationToken = default);

    Task<ScheduleResponse> GetScheduleAsync(Guid facultyId, CancellationToken cancellationToken = default);

    Task<TimeTableDto> GetFacultyTimeTableAsync(Guid facultyId, CancellationToken cancellationToken = default);

    Task<List<StudentInfoDto>> GetStudentsByFacultyIdAsync(Guid facultyId, CancellationToken cancellationToken = default);

    Task<ManagerDto> GetManagerInfoByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<IList<TeacherDto>> GetTeachersByFacultyIdAsync(Guid facultyId, CancellationToken cancellationToken = default);
}
