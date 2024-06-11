import { LessonType } from './my-day-gateway-view';

export interface ScheduleGatewayView {
    semesterId: string;
    startDate: string;
    endDate: string;
    evenWeek: WeekGatewayView;
    oddWeek: WeekGatewayView;
}

export interface WeekGatewayView {
    weekId: string;
    groupsSchedule: GroupScheduleGatewayView[];
}

export interface GroupScheduleGatewayView {
    groupId: string;
    days: DayScheduleGatewayView[];
}

export interface DayScheduleGatewayView {
    day: number;
    lessons: LessonGatewayView[];
}

export interface LessonGatewayView {
    id: string;
    lessonNumber: number;
    lessonName: string | null;
    teacherId: string;
    teacherName: string;
    roomId: string;
    roomName: string;
    type: LessonType;
    url: string | null;
    isOnline: boolean;

    // Format something like this: "09:40:00.0000000"
    startAt: string;
    endAt: string
}
