using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Schedule.Manager.Data.Models;
using EUniversity.Schedule.Manager.Data;
using MediatR;
using EUniversity.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using EUniversity.Schedule.Manager.Contract.Exceptions;

namespace EUniversity.Schedule.Manager.Api.Commands.Groups;

public class CreateGroupCommand(CreateGroupRequest request) : IRequest<Guid>
{
    public CreateGroupRequest Group { get; } = request;
}

public class CreateGroupCommandHandler(UniversityScheduleManagerContext db) : IRequestHandler<CreateGroupCommand, Guid>
{
    private readonly UniversityScheduleManagerContext _db = db.ThrowIfNull();

    public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var faculty = await _db.Faculties.FirstOrDefaultAsync(x => x.Id == request.Group.FacultyId, cancellationToken)
            ?? throw new EntityNotFoundException($"Faculty with ID {request.Group.FacultyId} not found.");
        
        var speciality = await _db.Specialities.FirstOrDefaultAsync(x => x.Id == request.Group.SpecialityId, cancellationToken)
            ?? throw new EntityNotFoundException($"Speciality with ID {request.Group.SpecialityId} not found.");

        var headStudent = request.Group.HeadStudentId.HasValue
            ? await _db.Students.FirstOrDefaultAsync(x => x.Id == request.Group.HeadStudentId.Value, cancellationToken: cancellationToken)
            : null;

        var curator = request.Group.CuratorId.HasValue
            ? await _db.Teachers.FirstOrDefaultAsync(x => x.Id == request.Group.CuratorId.Value, cancellationToken: cancellationToken)
            : null;

        var group = new Group
        {
            Id = Guid.NewGuid(),
            Course = request.Group.Course,
            Name = request.Group.Name,
            FacultyId = request.Group.FacultyId,
            SpecialityId = request.Group.SpecialityId,
            HeadStudentId = request.Group.HeadStudentId,
            CuratorId = request.Group.CuratorId,
            IsDisabled = false,
            Students = [],
            Lessons = []
        };

        if (request.Group.StudentIds != null && request.Group.StudentIds.Count > 0)
        {
            var students = await _db.Students
                .Where(s => request.Group.StudentIds.Contains(s.Id))
                .ToListAsync(cancellationToken);

            foreach (var student in students)
            {
                group.Students.Add(student);
            }
        }

        if (request.Group.LessonIds != null && request.Group.LessonIds.Count > 0)
        {
            var lessons = await _db.Lessons
                .Where(l => request.Group.LessonIds.Contains(l.Id))
                .ToListAsync(cancellationToken);
            
            foreach (var lesson in lessons)
            {
                group.Lessons.Add(lesson);
            }
        }

        await _db.Groups.AddAsync(group, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return group.Id;
    }
}