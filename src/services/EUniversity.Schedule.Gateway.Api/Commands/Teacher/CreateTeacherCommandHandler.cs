using EUniversity.Schedule.Manager.Client;
using EUniversity.Schedule.Manager.Contract.Requests;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Commands.Teacher;

public class CreateTeacherCommand(CreateTeacherRequest teacher) : IRequest<Guid>
{
    public CreateTeacherRequest Teacher { get; set; } = teacher;
}

public class CreateTeacherCommandHandler(IScheduleManagerClient scheduleManagerClient, ILogger<CreateTeacherCommandHandler> logger)
    : IRequestHandler<CreateTeacherCommand, Guid>
{
    public async Task<Guid> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await scheduleManagerClient.CreateTeacherAsync(request.Teacher, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occuder during creating Teacher");
            throw;
        }
    }
}
