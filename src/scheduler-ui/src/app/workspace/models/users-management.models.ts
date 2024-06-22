import { Permissions } from '../../core/providers/portal-user';

export interface CreateStudentRequest {
    email: string;
    groupId: string;
    facultyId: string;
    permissions: number[];
}

export interface CreateTeacherRequest {
    email: string;
    facultyIds: string[];
    fullName: string,
    posada: string
}

export const permissionsToNumbers = (permissions: Permissions[]) => {
    const array = [];

    permissions.forEach(permission => {
        if (permission === Permissions.FullAccess) array.push(8);

        if (permission === Permissions.FacultyFullAccess) array.push(4);

        if (permission === Permissions.ScheduleEditor) array.push(2);

        if (permission === Permissions.ScheduleViewer) array.push(1);
    });

    return array;
};
