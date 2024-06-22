using EUniversity.Schedule.Manager.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Schedule.Manager.Api.Commands.Users;

internal class DeleteUserCommand(Guid userId) : IRequest<Unit>
{
    public Guid UserId { get; set; } = userId;
}

internal class DeleteUserCommandHandler(UniversityScheduleManagerContext db, ILogger<DeleteUserCommandHandler> logger) : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var teacher = await db.Teachers.FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

        if (teacher != null)
        {
            logger.LogInformation("Remove teacher for UserId = '{UserId}'", request.UserId);
            db.Teachers.Remove(teacher);
        }

        var student = await db.Students.FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

        if (student != null)
        {
            logger.LogInformation("Remove student for UserId = '{UserId}'", request.UserId);
            db.Students.Remove(student);
        }

        var manager = await db.Managers.FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

        if (manager != null)
        {
            logger.LogInformation("Remove manager for UserId = '{UserId}'", request.UserId);
            db.Managers.Remove(manager);
        }

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
