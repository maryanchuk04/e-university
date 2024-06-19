import { Role } from '../models/role';

export interface PortalUser {
    id: string;
    permissions: Permissions[];
    role: Role;
}

export enum Permissions {
    FullAccess = 'FullAccess',
    ScheduleEditor = 'ScheduleEditor',
    ScheduleViewer = 'ScheduleViewer',
    NoAccess = 'NoAccess',
    FacultyFullAccess = 'FacultyFullAccess',
}
