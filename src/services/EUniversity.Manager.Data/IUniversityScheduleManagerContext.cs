using EUniversity.Schedule.Manager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Schedule.Manager.Data;

public interface IUniversityScheduleManagerContext
{
    public DbSet<Week> Weeks { get; set; }
    public DbSet<TimeTable> TimeTables { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Speciality> Specialities { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<LessonTime> LessonTimes { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Models.Schedule> Schedules { get; set; }
    public DbSet<Semester> Semesters { get; set; }
    public DbSet<Room> Rooms { get; set; }
}
