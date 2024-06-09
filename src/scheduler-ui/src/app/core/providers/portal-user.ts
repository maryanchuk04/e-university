import { Role } from '../models/role';

export interface PortalUser {
    id: string;
    permissions: string[];
    role: Role;
}
