using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Data;
using ManagerEntity = EUniversity.Schedule.Manager.Data.Models.Manager;
using MediatR;

namespace EUniversity.Schedule.Manager.Api.Commands.Manager;

public class CreateManagerCommand(ManagerDto manager) : IRequest<Guid>
{
    public ManagerDto Manager { get; set; } = manager;
}

public class CreateManagerCommandHandler(UniversityScheduleManagerContext db) : IRequestHandler<CreateManagerCommand, Guid>
{
    public async Task<Guid> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var manager = new ManagerEntity
        {
            Id = id,
            UserId = request.Manager.UserId,
        };

        if (request.Manager.FacultyId.HasValue)
        {
            manager.FacultyId = manager.FacultyId;
        }

        if (request.Manager.StundentId.HasValue)
        {
            manager.StundentId = manager.StundentId;
        }

        if (request.Manager.TeacherId.HasValue)
        {
            manager.TeacherId = manager.TeacherId;
        }

        await db.Managers.AddAsync(manager, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        return id;
    }
}
