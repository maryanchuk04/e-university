export interface StudentGatewayView {
    studentId: string;
    userId: string;

    fullName: string;
    picture: string;

    groupId: string;
    groupName: string;
    facultyId: string;
    facultyName: string;
    specialityId: string;
    specialityName: string;
    email: string;

    semesterId: string;

    isActive: boolean;
}
