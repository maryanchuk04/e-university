export interface TimeTable {
    id: string;
    lessonTimes: LessonTime[];
}

export interface LessonTime {
    id: string;
    lessonNumber: number;
    startAt: string;
    endAt: string;
}
