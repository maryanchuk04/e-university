export interface LessonView {
    id: string;
    name: string;

    startTime: string;
    endTime: string;

    type: LessonType;

    teacher: string;

    isOnline: boolean;
    url: string;
    audience: string;
}

export enum LessonType {
    Unknown = 0,
    Lecture = 1,
    Practice = 2,
}

export interface MyDay {
    date: string;

    lessons: LessonView[]
}

const testLessonViews: LessonView[] = [
    {
        id: "1",
        name: "Mathematics",
        startTime: "08:00",
        endTime: "09:30",
        type: LessonType.Lecture,
        teacher: "Dr. John Doe",
        isOnline: false,
        url: "",
        audience: "Room 101",
    },
    {
        id: "2",
        name: "Physics",
        startTime: "10:00",
        endTime: "11:30",
        type: LessonType.Practice,
        teacher: "Dr. Jane Smith",
        isOnline: true,
        url: "https://example.com/physics-class",
        audience: "",
    },
    {
        id: "3",
        name: "Chemistry",
        startTime: "12:00",
        endTime: "13:30",
        type: LessonType.Lecture,
        teacher: "Dr. Albert Brown",
        isOnline: false,
        url: "",
        audience: "Room 202",
    },
    {
        id: "4",
        name: "Information technologies",
        startTime: "13:40",
        endTime: "14:00",
        type: LessonType.Lecture,
        teacher: "Dr. Albert Brown",
        isOnline: false,
        url: "",
        audience: "Room 202",
    },
    {
        id: "5",
        name: "Information technologies 2",
        startTime: "13:40",
        endTime: "14:00",
        type: LessonType.Practice,
        teacher: "Dr. Albert Brown",
        isOnline: false,
        url: "",
        audience: "Room 202",
    }
];


export const mockMyDay: MyDay = {
    date: "2024-05-26",
    lessons: testLessonViews,
};

