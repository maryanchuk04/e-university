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

export enum LessonStatus {
    Now = 'now',
    Passed = 'passed',
    NotPassed = 'not-passed',
}
