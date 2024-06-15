import { DayOfWeek, WeekType } from './day-of-week';
import { LessonGatewayView } from './schedule';

export enum LessonType {
    Unknown = 0,
    Lecture = 1,
    Practice = 2,
    PracticeAndLecture = 3,
}

export interface MyDayGatewayView {
    date: string;
    currentWeekType: WeekType;
    lessons: LessonGatewayView[];
    today: DayOfWeek;

    nextDay: DayOfWeek;
    nextDayLessons: LessonGatewayView[];
    nextDayWeekType: WeekType;
}
