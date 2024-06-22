using EUniversity.Schedule.Manager.Contract.Models;
using EUniversity.Schedule.Manager.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Schedule.Manager.Api.Queries.Groups;

public class GetGroupsInfoQuery(Guid facultyId) : IRequest<List<GroupInfoDto>> 
{
    public Guid FacultyId { get; set; } = facultyId;
}

public class GetGroupsInfoQueryHandler(UniversityScheduleManagerContext db) : IRequestHandler<GetGroupsInfoQuery, List<GroupInfoDto>>
{
    public async Task<List<GroupInfoDto>> Handle(GetGroupsInfoQuery request, CancellationToken cancellationToken)
    {
        var groups = await db.Groups
            .AsNoTracking()
            .AsSplitQuery()
            .Include(g => g.Speciality)
            .Where(g => g.FacultyId == request.FacultyId)
            .Select(g => new GroupInfoDto
            {
                Id = g.Id,
                FacultyId = g.FacultyId,
                Name = g.Name,
                SpecialityName = g.Speciality.Name
            }).ToListAsync(cancellationToken);

        return groups;
    }
}
