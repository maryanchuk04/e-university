using EUniversity.Authorization.Client;
using EUniversity.Schedule.Gateway.Contract.Responses;
using EUniversity.Schedule.Manager.Client;
using MediatR;

namespace EUniversity.Schedule.Gateway.Api.Queries.Users.Teachers;
public class GetTeachersQuery(Guid facultyId) : IRequest<List<TeacherResponse>>
{
    public Guid FacultyId { get; set; } = facultyId;
}

public class GetTeachersQueryHandler(IScheduleManagerClient scheduleManagerClient, IAuthorizationClient authorizationClient) : IRequestHandler<GetTeachersQuery, List<TeacherResponse>>
{
    public async Task<List<TeacherResponse>> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
    {
        var teachers = await scheduleManagerClient.GetTeachersByFacultyIdAsync(request.FacultyId, cancellationToken);

        var users = await authorizationClient.GetUsersAsync(cancellationToken);

        return teachers.Join(users, t => t.UserId, u => u.Id, (teacher, user) =>
        {
            return new TeacherResponse
            {
                Picture = user.Picture,
                UserId = user.Id,
                FullName = teacher.FullName,
                Position = teacher.Position,
                Faculties = teacher.Faculties,
                Id = teacher.Id,
                IsActive = user.IsActive,
            };
        }).ToList();
    }
}
