using EUniversity.Authorization.Client;
using EUniversity.Schedule.Gateway.Contract.Models;
using EUniversity.Schedule.Gateway.Contract.Providers;
using EUniversity.Schedule.Manager.Client;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Queries.Users.Students;

public class GetStudentsQuery(Guid facultyId) : IRequest<List<StudentGatewayView>>
{
    public Guid FacultyId { get; set; } = facultyId;
}

public class GetStudentsQueryHandler(
    IScheduleManagerClient scheduleManagerClient,
    IAuthorizationClient authorizationClient)
    : IRequestHandler<GetStudentsQuery, List<StudentGatewayView>>
{
    public async Task<List<StudentGatewayView>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var systemUsersTask = authorizationClient.GetUsersAsync(cancellationToken);

        var facultyStudentsTask = scheduleManagerClient.GetStudentsByFacultyIdAsync(request.FacultyId, cancellationToken);

        var systemUsers = await systemUsersTask;
        var students = await facultyStudentsTask;

        return students.Join(systemUsers, student => student.UserId, user => user.Id, (student, user) =>
        {
            return new StudentGatewayView
            {
                UserId = user.Id,
                FacultyId = student.FacultyId,
                FacultyName = student.FacultyName,
                StudentId = student.Id,
                FullName = user.FullName,
                SpecialityName = student.SpecialityName,
                SpecialityId = student.SpecialityId,
                GroupId = student.GroupId,
                GroupName = student.GroupName,
                Picture = user.Picture,
                Email = user.Email,
                IsActive = user.IsActive,
            };      
        }).ToList();
    }
}
