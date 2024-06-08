namespace EUniversity.Schedule.Manager.Contract.Exceptions.TimeTable;

public class TimeTableForFacultyAlreadyExistException(Guid facultyId)
    : Exception($"TimeTable for Faculty = '{facultyId}' already exist.")
{
}
