using EUniversity.Schedule.Manager.Contract.Exceptions;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Schedule.Manager.Data;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FacultyEntity = EUniversity.Schedule.Manager.Data.Models.Faculty;

namespace EUniversity.Schedule.Manager.Api.Commands.Faculty;

public class CreateFacultyCommand(CreateFacultyRequest request) : IRequest<Guid>
{
    public CreateFacultyRequest Faculty { get; } = request.ThrowIfNull();
}

public class CreateFacultyCommandHandler(UniversityScheduleManagerContext db) : IRequestHandler<CreateFacultyCommand, Guid>
{
    private readonly UniversityScheduleManagerContext _db = db.ThrowIfNull();

    public async Task<Guid> Handle(CreateFacultyCommand command, CancellationToken cancellationToken)
    {
        if (await _db.Faculties.AnyAsync(f => f.Name.Equals(command.Faculty.Name, StringComparison.CurrentCultureIgnoreCase), cancellationToken: cancellationToken))
            throw new EntityAlreadyExistException(nameof(FacultyEntity), command.Faculty.Name);

        var faculty = new FacultyEntity
        {
            Id = Guid.NewGuid(),
            Name = command.Faculty.Name,
            Description = command.Faculty.Description,
            Adress = command.Faculty.Address,
            DeanId = command.Faculty.DeanId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _db.Faculties.AddAsync(faculty, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return faculty.Id;
    }
}