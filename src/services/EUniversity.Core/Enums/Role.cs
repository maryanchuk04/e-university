namespace EUniversity.Core.Enums;

[Flags]
public enum Role
{
    User = 0,
    Student = 1,
    Teacher = 2,
    ScheduleAdmin = 4,
    FacultyAdmin = 8,
    Admin = 16,
}