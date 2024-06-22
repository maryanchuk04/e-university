using EUniversity.Authorization.Client;
using EUniversity.Core.Enums;
using EUniversity.Schedule.Gateway.Contract.Requests;
using EUniversity.Schedule.Manager.Client;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Commands.Students;

public class CreateStudentCommand(CreateStudentRequest student) : IRequest<Guid>
{
    public CreateStudentRequest Student { get; set; } = student;
}

public class CreateStudentCommandHandler(IScheduleManagerClient scheduleManagerClient, IAuthorizationClient authorizationClient) : IRequestHandler<CreateStudentCommand, Guid>
{
    public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var userId = await authorizationClient.CreateNonActiveUserAsync(new Authorization.Contract.Requests.CreateUserRequest
        {
            Email = request.Student.Email,
            Role = Core.Enums.Role.Student,
            Permissions = request.Student.Permissions,
        }, cancellationToken);

        var studentId = await scheduleManagerClient.CreateStudentAsync(new Manager.Contract.Requests.CreateStudentRequest
        {
            UserId = userId,
            GroupId = request.Student.GroupId,
            FacultyId = request.Student.FacultyId,
        }, cancellationToken);

        if (IsAdmin(request.Student.Permissions))
        {
            var manager = new Manager.Contract.Models.ManagerDto
            {
                UserId = userId,
                StundentId = studentId,
            };

            if (!request.Student.Permissions.Contains(PermissionType.FullAccess))
                manager.FacultyId = request.Student.FacultyId;

            await scheduleManagerClient.CreateManagerAsync(manager, cancellationToken);
        }

        return studentId;
    }

    private static bool IsAdmin(PermissionType[] permissions)
        => permissions.Contains(PermissionType.FullAccess) || permissions.Contains(PermissionType.FacultyFullAccess);
}
