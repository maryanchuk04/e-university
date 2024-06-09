namespace EUniversity.Core.Enums;

[Flags]
public enum PermissionType
{
    NoAccess = 0,
    ScheduleViewer = 1,
    ScheduleEditor = 2,
    FacultyFullAccess = 4,
    FullAccess = 8,
}