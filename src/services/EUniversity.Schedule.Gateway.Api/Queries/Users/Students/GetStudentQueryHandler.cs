using EUniversity.Authorization.Client;
using EUniversity.Schedule.Gateway.Contract.Models;
using EUniversity.Schedule.Gateway.Contract.Providers;
using EUniversity.Schedule.Manager.Client;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Queries.Users.Students;

public class GetCurrentStudentQuery : IRequest<StudentGatewayView> { }

public class GetStudentQueryHandler(
    IPortalUserProvider portalUserProvider,
    IScheduleManagerClient scheduleManagerClient,
    IAuthorizationClient authorizationClient) : IRequestHandler<GetCurrentStudentQuery, StudentGatewayView>
{

    public async Task<StudentGatewayView> Handle(GetCurrentStudentQuery request, CancellationToken cancellationToken)
    {
        var user = portalUserProvider.GetPortalUser();

        var authUserTask = authorizationClient.GetUserAsync(user.UserId, cancellationToken);
        var studentTask = scheduleManagerClient.GetStudentInfoByUserIdAsync(user.UserId, cancellationToken);

        var authUser = await authUserTask;
        var student = await studentTask;

        return new StudentGatewayView
        {
            UserId = authUser.Id,
            Picture = authUser.Picture,
            FullName = authUser.FullName,

            StudentId = student.Id,
            GroupId = student.GroupId,
            GroupName = student.GroupName,
            FacultyName = student.FacultyName,
            FacultyId = student.FacultyId,
            SpecialityId = student.SpecialityId,
            SpecialityName = student.SpecialityName,
        };
    }
}
