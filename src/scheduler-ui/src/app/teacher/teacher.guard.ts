import { Injectable } from '@angular/core';

import { Role } from '../core/models/role';
import { UserProvider } from '../core/providers/user.provider';

@Injectable({
    providedIn: 'root',
})
export class TeacherGuard {
    constructor(private userProvider: UserProvider) {}

    canActivate(): boolean {
        const currentUser = this.userProvider.getCurrentUser();

        if (currentUser && currentUser.role === Role.Teacher)
            return true;

        return false;
    }
}
