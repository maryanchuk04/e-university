using EUniversity.Schedule.Manager.Contract.Exceptions;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Schedule.Manager.Data;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SpecialityEntity = EUniversity.Schedule.Manager.Data.Models.Speciality;

namespace EUniversity.Schedule.Manager.Api.Commands.Speciality;

public class CreateSpecialityCommand(CreateSpecialityRequest request) : IRequest<Guid>
{
    public CreateSpecialityRequest Speciality { get; } = request;
}

public class CreateSpecialityCommandHandler(UniversityScheduleManagerContext db) : IRequestHandler<CreateSpecialityCommand, Guid>
{
    private readonly UniversityScheduleManagerContext _db = db.ThrowIfNull();

    public async Task<Guid> Handle(CreateSpecialityCommand request, CancellationToken cancellationToken)
    {
        var faculty = await _db.Faculties.FirstOrDefaultAsync(x => x.Id == request.Speciality.FacultyId, cancellationToken)
            ?? throw new EntityNotFoundException($"Faculty with ID {request.Speciality.FacultyId} not found.");

        var speciality = new SpecialityEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Speciality.Name,
            Description = request.Speciality.Description,
            FacultyId = request.Speciality.FacultyId,
            Faculty = faculty
        };

        await _db.Specialities.AddAsync(speciality, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return speciality.Id;
    }
}