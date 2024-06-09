import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { UserProvider } from '../providers/user.provider';

@Injectable({
    providedIn: 'root',
})
export class AuthGuard {
    constructor(private userProvider: UserProvider, private router: Router) {}

    canActivate(): boolean {
        const currentUser = this.userProvider.getCurrentUser();
        if (currentUser) return true;

        this.router.navigate(['/authenticate']);
        return false;
    }
}
