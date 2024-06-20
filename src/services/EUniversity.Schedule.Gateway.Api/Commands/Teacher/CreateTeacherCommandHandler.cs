using EUniversity.Authorization.Client;
using EUniversity.Schedule.Manager.Client;
using EUniversity.Schedule.Manager.Contract.Requests;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Commands.Teacher;

public class CreateTeacherCommand(CreateTeacherRequest teacher) : IRequest<Guid>
{
    public CreateTeacherRequest Teacher { get; set; } = teacher;
}

public class CreateTeacherCommandHandler(IScheduleManagerClient scheduleManagerClient, ILogger<CreateTeacherCommandHandler> logger, IAuthorizationClient authorizationClient)
    : IRequestHandler<CreateTeacherCommand, Guid>
{
    public async Task<Guid> Handle(CreateTeacherCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var userId = await authorizationClient.CreateNonActiveUserAsync(new Authorization.Contract.Requests.CreateUserRequest
            {
                Email = command.Teacher.Email,
                Role = Core.Enums.Role.Teacher,
                Permissions = [Core.Enums.PermissionType.ScheduleViewer]
            }, cancellationToken);

            command.Teacher.UserId = userId;

            return await scheduleManagerClient.CreateTeacherAsync(command.Teacher, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occuder during creating Teacher");
            throw;
        }
    }
}
