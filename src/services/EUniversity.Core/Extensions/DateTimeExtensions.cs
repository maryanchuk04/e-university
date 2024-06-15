using DayOfWeek = EUniversity.Core.Enums.DayOfWeek;

namespace EUniversity.Core.Extensions;

public static class DateTimeExtensions
{
    /// <summary>
    /// Get day of week by some DateTime
    /// </summary>
    /// <param name="dateTime">Date time</param>
    /// <returns>Day of week</returns>
    public static DayOfWeek GetDayOfWeek(this DateTime dateTime)
    {
        var dayOfWeekIndex = (int)dateTime.DayOfWeek;

        // Adjust index to start from Monday (1 = Monday, ..., 7 = Sunday)
        dayOfWeekIndex = dayOfWeekIndex == 0 ? 7 : dayOfWeekIndex;

        return (DayOfWeek)dayOfWeekIndex;
    }
}
