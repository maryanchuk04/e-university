import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { UserProvider } from '../providers/user.provider';

@Injectable({
    providedIn: 'root',
})
export class AdminGuard {
    constructor(private userProvider: UserProvider, private router: Router) {}

    canActivate(): boolean {
        const currentUser = this.userProvider.getCurrentUser();

        if (!currentUser) return false;

        if (!this.userProvider.hasAdminAccess())
            return false;

        return true;
    }
}
