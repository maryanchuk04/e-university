import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { Role } from '../models/role';
import { UserProvider } from '../providers/user.provider';

@Injectable({
    providedIn: 'root',
})
export class AdminGuard {
    constructor(private userProvider: UserProvider, private router: Router) {}

    canActivate(): boolean {
        const currentUser = this.userProvider.getCurrentUser();

        if (!currentUser) return false;

        if (currentUser.role === Role.User || currentUser.role === Role.Student || currentUser.role === Role.Teacher)
            return false;

        return true;
    }
}
