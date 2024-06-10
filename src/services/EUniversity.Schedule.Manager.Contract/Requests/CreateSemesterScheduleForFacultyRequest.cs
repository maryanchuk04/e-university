using EUniversity.Schedule.Manager.Contract.Models.SemesterSchedule;

namespace EUniversity.Schedule.Manager.Contract.Requests;

public class CreateSemesterScheduleForFacultyRequest
{
    public int SemesterNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid FacultyId { get; set; }
    public ScheduleDto Schedule { get; set; }
}