using EUniversity.Schedule.Manager.Data.Models;
using EUniversity.Schedule.Manager.Data.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Schedule.Manager.Data;

public class UniversityManagerContext : DbContext, IUniversityScheduleManagerContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

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
