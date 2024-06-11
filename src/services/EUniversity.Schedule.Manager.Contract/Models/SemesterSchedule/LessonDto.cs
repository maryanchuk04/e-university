using EUniversity.Core.Enums;

namespace EUniversity.Schedule.Manager.Contract.Models.SemesterSchedule;

public class LessonDto
{
    public Guid Id { get; set; }
    public int LessonNumber { get; set; }
    public string LessonName { get; set; }
    public Guid TeacherId { get; set; }
    public string? TeacherName { get; set; }
    public Guid RoomId { get; set; }
    public string? RoomName { get; set; }
    public LessonType? Type { get; set; } = LessonType.LectureAndPractice;
    public string? Url { get; set; }
    public bool? IsOnline { get; set; } = false;

    public TimeOnly? StartAt { get; set; }
    public TimeOnly? EndAt { get; set; }
}