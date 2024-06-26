import { jwtDecode } from 'jwt-decode';

import { Injectable } from '@angular/core';

import { AuthService } from '../../auth/services/auth.service';
import { Role } from '../models/role';
import { Permissions, PortalUser } from './portal-user';

@Injectable({ providedIn: 'root' })
export class UserProvider {
    private claimsKey = 'https://e-university.ua.com/';

    constructor(private authService: AuthService) {}

    getCurrentUser(): PortalUser | null {
        const token = this.authService.getAccessToken();

        if (!token) {
            return null;
        }

        try {
            const decoded: any = jwtDecode(token);

            const user: PortalUser = {
                id: decoded['userId'],
                permissions: this.parsePermissions(
                    decoded[`${this.claimsKey}permissions`]
                ),
                role: decoded[`${this.claimsKey}roles`] as Role,
            };

            return user;
        } catch (error) {
            console.error('Error decoding token:', error);
            return null;
        }
    }

    hasAdminAccess() {
        const user = this.getCurrentUser();

        if (!user) return false;

        return (
            user.role === Role.Admin ||
            user.permissions.includes(Permissions.FacultyFullAccess) ||
            user.permissions.includes(Permissions.FullAccess)
        );
    }

    hasAccessToDashboard(): boolean {
        const user = this.getCurrentUser();

        if (!user) return false;

        return user.role === Role.Student || user.role === Role.Admin;
    }

    private parsePermissions = (decodedPermissions: any) => {
        if (typeof decodedPermissions === 'string') return [decodedPermissions];
        if (Array.isArray(decodedPermissions)) return decodedPermissions;

        return [];
    };
}
