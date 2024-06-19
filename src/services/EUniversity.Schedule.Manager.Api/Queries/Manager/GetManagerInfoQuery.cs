using EUniversity.Schedule.Manager.Contract.Exceptions;
using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Schedule.Manager.Api.Queries.Manager;

public class GetManagerInfoQuery(Guid UserId) : IRequest<ManagerDto>
{
    public Guid UserId { get; set; } = UserId;
}

public class GetManagerInfoQueryHandler(UniversityScheduleManagerContext db) : IRequestHandler<GetManagerInfoQuery, ManagerDto>
{
    public async Task<ManagerDto> Handle(GetManagerInfoQuery request, CancellationToken cancellationToken)
    {
        var manager = await db.Managers
            .AsSingleQuery()
            .Include(m => m.Faculty)
            .Include(m => m.Student)
            .Include(m => m.Teacher)
            .FirstOrDefaultAsync(m => m.UserId == request.UserId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Manager), nameof(request.UserId), request.UserId.ToString());

        return new ManagerDto
        {
            ManagerId = manager.Id,
            TeacherId = manager.TeacherId,
            FacultyId = manager.FacultyId,
            FacultyName = manager.Faculty?.Name,
            StundentId = manager.StundentId,
            UserId = manager.UserId,
        };
    }
}
