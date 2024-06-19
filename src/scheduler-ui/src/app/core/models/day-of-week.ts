export enum DayOfWeek {
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6,
    Sunday = 7,
}

export const getNextDay = (day: DayOfWeek) =>
    day === DayOfWeek.Sunday ? DayOfWeek.Monday : ((day + 1) as DayOfWeek);

export const workingDays = [
    DayOfWeek.Monday,
    DayOfWeek.Tuesday,
    DayOfWeek.Wednesday,
    DayOfWeek.Thursday,
    DayOfWeek.Friday,
];

export const week = [...workingDays, DayOfWeek.Saturday, DayOfWeek.Sunday];

export enum WeekType {
    Even = 0,
    Odd = 1,
}
