﻿using EUniversity.Core.Http;
using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Schedule.Manager.Contract.Responses;
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
    #region Routes

    private const string FacultyRoute = "/api/faculty";
    private const string TeacherRoute = "/api/teacher";
    private const string StudentRoute = "/api/student";
    private const string GroupRoute = "/api/groups";
    private const string SpecialityRoute = "/api/speciality";
    private const string ScheduleRoute = "/api/schedule";
    private const string ManagerRoute = "/api/manager";
    private const string UserRoute = "/api/user";

    #endregion

    #region Faculty

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

    public Task<TimeTableDto> GetFacultyTimeTableAsync(Guid facultyId, CancellationToken cancellationToken = default)
    {
        return GetAsync<TimeTableDto>($"{FacultyRoute}/{facultyId}/timetable", cancellationToken: cancellationToken);
    }

    #endregion

    #region Teacher

    public Task<Guid> CreateTeacherAsync(CreateTeacherRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<CreateTeacherRequest, Guid>(TeacherRoute, request, cancellationToken: cancellationToken);
    }

    public Task<IList<TeacherDto>> GetTeachersByFacultyIdAsync(Guid facultyId, CancellationToken cancellationToken = default)
    {
        return GetAsync<IList<TeacherDto>>($"{TeacherRoute}/faculty/{facultyId}", cancellationToken: cancellationToken);
    }

    #endregion

    #region Students

    public Task<StudentInfoDto> GetStudentInfoByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var route = $"{StudentRoute}/{userId}";

        return GetAsync<StudentInfoDto>(route, cancellationToken: cancellationToken);
    }

    public Task<List<StudentInfoDto>> GetStudentsByFacultyIdAsync(Guid facultyId, CancellationToken cancellationToken = default)
    {
        var route = $"{StudentRoute}/faculty/{facultyId}";
        return GetAsync<List<StudentInfoDto>>(route, cancellationToken: cancellationToken);
    }

    public Task<Guid> CreateStudentAsync(CreateStudentRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<CreateStudentRequest, Guid>(StudentRoute, request, cancellationToken: cancellationToken);
    }

    #endregion

    #region Groups

    public Task<Guid> CreateGroupAsync(CreateGroupRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<CreateGroupRequest, Guid>(GroupRoute, request, cancellationToken: cancellationToken);
    }

    public Task<List<GroupInfoDto>> GetGroupsInfoDtosAsync(Guid facultyId, CancellationToken cancellationToken = default)
    {
        return GetAsync<List<GroupInfoDto>>($"{GroupRoute}/faculty/{facultyId}", cancellationToken: cancellationToken);
    }

    #endregion

    #region Speciality

    public Task<Guid> CreateSpecialityAsync(CreateSpecialityRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync<CreateSpecialityRequest, Guid>(SpecialityRoute, request, cancellationToken: cancellationToken);
    }

    #endregion

    #region Schedule

    public Task<ScheduleResponse> GetScheduleAsync(Guid facultyId, CancellationToken cancellationToken = default)
    {
        var route = $"{ScheduleRoute}/faculty/{facultyId}";

        return GetAsync<ScheduleResponse>(route, cancellationToken: cancellationToken);
    }

    #endregion

    #region Manager

    public Task<ManagerDto> GetManagerInfoByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return GetAsync<ManagerDto>($"{ManagerRoute}/{userId}", cancellationToken: cancellationToken);
    }

    public Task<Guid> CreateManagerAsync(ManagerDto request, CancellationToken cancellationToken = default)
    {
        return PostAsync<ManagerDto, Guid>(ManagerRoute, request, cancellationToken: cancellationToken);
    }

    #endregion

    #region User

    public Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return DeleteJsonAsync<object>($"{UserRoute}/{userId}", cancellationToken: cancellationToken);
    }

    #endregion
}
