namespace EUniversity.Schedule.Manager.Contract.Models;

public class LessonDto
{
    public Guid Id { get; set; }
    public int LessonNumber { get; set; }
    public string LessonName { get; set; }
    public Guid TeacherId { get; set; }
    public string TeacherName { get; set; }
    public Guid RoomId { get; set; }
    public string RoomName { get; set; }
}