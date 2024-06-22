using EUniversity.Authorization.Data.Models;
using EUniversity.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Role = EUniversity.Authorization.Data.Models.Role;

namespace EUniversity.Authorization.Data.Extensions;

internal static class ModelBuilderExtensions
{
    public static void SeedRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData([
            new Role { Id = Core.Enums.Role.Admin, Name = "Admin", Description = "Administrator role. Full Access" },
            new Role { Id = Core.Enums.Role.Student, Name = "Student", Description = "Student role" },
            new Role { Id = Core.Enums.Role.Teacher, Name = "Teacher", Description = "Teacher role" },
            new Role { Id = Core.Enums.Role.FacultyAdmin, Name = "Faculty Admin", Description = "Faculty administrator. Has admin role on own faculty." },
            new Role { Id = Core.Enums.Role.ScheduleAdmin, Name = "Schedule Admin", Description = "Schedule administrator"},
            new Role { Id = Core.Enums.Role.User, Name = "User", Description = "User" },]);
    }

    public static void SeedDefaultPermissions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Permission>().HasData([
            // For Admin
            new Permission { Id = 1, Type = PermissionType.FullAccess, Name = "FullAccess", },
            new Permission { Id = 2, Type = PermissionType.ScheduleEditor, Name = "ScheduleEditor", },
            // For student
            new Permission { Id = 3, Type = PermissionType.ScheduleViewer, Name = "ScheduleViewer", },
            // No access
            new Permission { Id = 4, Type = PermissionType.NoAccess, Name = "NoAccess", },
            // For Faculty admin
            new Permission { Id = 5, Type = PermissionType.FacultyFullAccess, Name = "FacultyFullAccess" }]);
    }
}
