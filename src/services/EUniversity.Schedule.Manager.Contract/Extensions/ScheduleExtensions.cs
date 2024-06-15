using EUniversity.Schedule.Manager.Contract.Models.SemesterSchedule;

namespace EUniversity.Schedule.Manager.Contract.Extensions;

public static class ScheduleExtensions
{
    public static DayScheduleDto? GetNextDayWithLessonsForGroup(this List<GroupScheduleDto> groupSchedules, Guid groupId)
    {
        var groupSchedule = groupSchedules.FirstOrDefault(gs => gs.GroupId == groupId);

        if (groupSchedule == null)
            return null;

        return groupSchedule.Days.OrderBy(d => d.Day).FirstOrDefault(d => d.Lessons.Count > 0);
    }
}
