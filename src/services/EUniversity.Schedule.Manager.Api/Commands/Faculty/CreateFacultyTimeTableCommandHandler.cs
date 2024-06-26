﻿using EUniversity.Schedule.Manager.Contract.Exceptions;
using EUniversity.Schedule.Manager.Contract.Exceptions.TimeTable;
using EUniversity.Schedule.Manager.Contract.Requests;
using EUniversity.Schedule.Manager.Data;
using EUniversity.Schedule.Manager.Data.Models;
using EUniversity.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeTableEntity = EUniversity.Schedule.Manager.Data.Models.TimeTable;

namespace EUniversity.Schedule.Manager.Api.Commands.Faculty;

internal class CreateFacultyTimeTableCommand : IRequest<Unit>
{
    public Guid FacultyId { get; }
    public List<LessonTimeDetails> LessonTimeDetails { get; }

    public CreateFacultyTimeTableCommand(Guid facultyId, CreateTimeTableRequest request)
    {
        request.ThrowIfNull();

        FacultyId = facultyId.ThrowIfNullOrDefault();
        LessonTimeDetails = request.LessonTimes;
    }
}

internal class CreateFacultyTimeTableCommandHandler(UniversityScheduleManagerContext db) : IRequestHandler<CreateFacultyTimeTableCommand, Unit>
{
    private readonly UniversityScheduleManagerContext _db = db.ThrowIfNull();

    public async Task<Unit> Handle(CreateFacultyTimeTableCommand command, CancellationToken cancellationToken)
    {
        var faculty = await _db.Faculties
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == command.FacultyId, cancellationToken)
                ?? throw new EntityNotFoundException(nameof(Faculty), nameof(command.FacultyId), command.FacultyId.ToString());

        if (await _db.TimeTables.AnyAsync(tt => tt.FacultyId == command.FacultyId, cancellationToken))
            throw new TimeTableForFacultyAlreadyExistException(command.FacultyId);

        var lessonTimes = command.LessonTimeDetails.Select(lessonTimeDetails => new LessonTime
        {
            Id = Guid.NewGuid(),
            LessonNumber = lessonTimeDetails.LessonNumber,
            StartAt = lessonTimeDetails.StartAt,
            EndAt = lessonTimeDetails.EndAt,
        }).ToList();

        
        var ttId = Guid.NewGuid();
        faculty.TimeTableId = ttId;

        var timeTable = new TimeTableEntity
        {
            Id = ttId,
            FacultyId = faculty.Id,
            LessonTimes = lessonTimes,
        };

        _db.Faculties.Update(faculty);
        await _db.TimeTables.AddAsync(timeTable, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}