namespace EUniversity.Core.Enums;

[Flags]
public enum Role
{
    User = 0,
    Student = 1,
    ScheduleAdmin = 2,
    FacultyAdmin = 4,
    Admin = 8,
}