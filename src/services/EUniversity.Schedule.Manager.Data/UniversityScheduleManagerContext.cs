using EUniversity.Schedule.Manager.Data.Models;
using EUniversity.Schedule.Manager.Data.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Schedule.Manager.Data;

public class UniversityScheduleManagerContext(DbContextOptions options)
    : DbContext(options), IUniversityScheduleManagerContext
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
    public DbSet<TeacherFaculty> TeacherFaculties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Group relationships
        modelBuilder.Entity<Group>()
            .HasOne(g => g.Curator)
            .WithMany()
            .HasForeignKey(g => g.CuratorId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Group>()
            .HasOne(g => g.Faculty)
            .WithMany()
            .HasForeignKey(g => g.FacultyId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Group>()
            .HasOne(g => g.HeadStudent)
            .WithMany()
            .HasForeignKey(g => g.HeadStudentId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Group>()
            .HasOne(g => g.Speciality)
            .WithMany()
            .HasForeignKey(g => g.SpecialityId)
            .OnDelete(DeleteBehavior.NoAction);

        // Lesson relationships
        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Group)
            .WithMany(g => g.Lessons)
            .HasForeignKey(l => l.GroupId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.LessonTime)
            .WithMany(lt => lt.Lessons)
            .HasForeignKey(l => l.LessonTimeId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Room)
            .WithMany(r => r.Lessons)
            .HasForeignKey(l => l.RoomId);

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Subject)
            .WithMany(s => s.Lessons)
            .HasForeignKey(l => l.SubjectId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Teacher)
            .WithMany(t => t.Lessons)
            .HasForeignKey(l => l.TeacherId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Week)
            .WithMany(w => w.Lessons)
            .HasForeignKey(l => l.WeekId)
            .OnDelete(DeleteBehavior.NoAction);

        // LessonTime and TimeTable relationship
        modelBuilder.Entity<LessonTime>()
            .HasOne(lt => lt.TimeTable)
            .WithMany(tt => tt.LessonTimes)
            .HasForeignKey(lt => lt.TimeTableId)
            .OnDelete(DeleteBehavior.NoAction);

        // Room and Faculty relationship
        modelBuilder.Entity<Room>()
            .HasOne(r => r.Faculty)
            .WithMany(f => f.Rooms)
            .HasForeignKey(r => r.FacultyId)
            .OnDelete(DeleteBehavior.NoAction);

        // Schedule and Week relationships
        modelBuilder.Entity<Models.Schedule>()
            .HasOne(s => s.EvenWeek)
            .WithMany()
            .HasForeignKey(s => s.EvenWeekId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Models.Schedule>()
            .HasOne(s => s.OddWeek)
            .WithMany()
            .HasForeignKey(s => s.OddWeekId)
            .OnDelete(DeleteBehavior.NoAction);

        // Semester and Schedule relationship
        modelBuilder.Entity<Semester>()
            .HasOne(s => s.Schedule)
            .WithMany()
            .HasForeignKey(s => s.ScheduleId)
            .OnDelete(DeleteBehavior.NoAction);

        // Speciality and Faculty relationship
        modelBuilder.Entity<Speciality>()
            .HasOne(s => s.Faculty)
            .WithMany(f => f.Specialities)
            .HasForeignKey(s => s.FacultyId)
            .OnDelete(DeleteBehavior.NoAction);

        // Student and Group relationship
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Group)
            .WithMany(g => g.Students)
            .HasForeignKey(s => s.GroupId);

        // TimeTable and Faculty relationship
        modelBuilder.Entity<TimeTable>()
            .HasOne(tt => tt.Faculty)
            .WithOne(f => f.TimeTable)
            .HasForeignKey<TimeTable>(t => t.FacultyId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    #region Context Modifications

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;

            if (entity.State == EntityState.Added)
            {
                ((BaseEntity)entity.Entity).CreatedAt = now;
            }
            ((BaseEntity)entity.Entity).UpdatedAt = now;
        }
    }

    #endregion
}
