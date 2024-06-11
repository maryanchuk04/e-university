export interface TimeTableGatewayView {
    id: string;
    lessonTimes: LessonTimeGatewayView[];
}

export interface LessonTimeGatewayView {
    id: string;
    lessonNumber: number;
    startAt: string; // ISO 8601 string for TimeOnly
    endAt: string; // ISO 8601 string for TimeOnly
}
