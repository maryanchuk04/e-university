export interface Teacher {
    id: string;
    fullName: string;
    position: string;
    userId: string;
}

export interface TeacherGatewayView {
    id: string;
    fullName: string;
    position: string;
    userId: string;
    picture: string;
    isActive: string;
    faculties: any
}

