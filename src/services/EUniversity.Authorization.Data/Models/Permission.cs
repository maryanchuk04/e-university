namespace EUniversity.Authorization.Data.Models;

/// <summary>
/// The permissions entity.
/// </summary>
/// <example>"FullAccess" or "ScheduleViewer" etc...</example>
public class Permission
{
    public int Id { get; set; }

    public PermissionType Type { get; set; }

    public string Name { get; set; }
}

[Flags]
public enum PermissionType
{
    NoAccess = 0,
    ScheduleViewer = 1,
    ScheduleEditor = 2,
    FacultyFullAccess = 4,
    FullAccess = 8,
}