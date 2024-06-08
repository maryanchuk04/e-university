namespace EUniversity.Schedule.Manager.Contract.Models;

public class TimeTableDto
{
    public Guid Id { get; set; }
    public List<LessonTimeDto> LessonTimes { get; set; }
}

public class LessonTimeDto
{
    public Guid Id { get; set; }
    public int LessonNumber { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
}