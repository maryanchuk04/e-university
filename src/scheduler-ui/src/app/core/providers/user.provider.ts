import { jwtDecode } from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';

import { Injectable } from '@angular/core';

import { Role } from '../models/role';
import { PortalUser } from './portal-user';

@Injectable({ providedIn: 'root' })
export class UserProvider {
    constructor(private cookieService: CookieService) {}

    getCurrentUser(): PortalUser | null {
        const token = this.cookieService.get('e_access_token');

        if (!token) {
            return null;
        }

        try {
            const decoded: any = jwtDecode(token);

            const user: PortalUser = {
                id: decoded['https://e-university.ua.com/userId'],
                permissions: this.parsePermissions(
                    decoded['https://e-university.ua.com/permissions']
                ),
                role: decoded['https://e-university.ua.com/roles'] as Role,
            };

            return user;
        } catch (error) {
            console.error('Error decoding token:', error);
            return null;
        }
    }

    private parsePermissions = (decodedPermissions: any) => {
        if (typeof decodedPermissions === 'string') return [decodedPermissions];
        if (Array.isArray(decodedPermissions)) return decodedPermissions;

        return [];
    };
}
