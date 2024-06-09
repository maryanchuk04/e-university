﻿namespace EUniversity.Schedule.Manager.Contract.Models;

public class TimeTableDto
{
    public Guid Id { get; set; }
    public List<LessonTimeDto> LessonTimes { get; set; }
}

public class LessonTimeDto
{
    public LessonTimeDto() { }

    public LessonTimeDto(Guid id, int lessonNumber, TimeOnly startAt, TimeOnly endAt)
    {
        Id = id;
        LessonNumber = lessonNumber;
        StartAt = startAt;
        EndAt = endAt;
    }

    public Guid Id { get; set; }
    public int LessonNumber { get; set; }
    public TimeOnly StartAt { get; set; }
    public TimeOnly EndAt { get; set; }
}