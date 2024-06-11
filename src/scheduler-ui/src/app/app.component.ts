import { PrimeNGConfig } from 'primeng/api';

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserProvider } from './core/providers/user.provider';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit{
    constructor(private primengConfig: PrimeNGConfig, private userProvider: UserProvider, private router: Router) {}

     ngOnInit() {
        this.primengConfig.ripple = true;

        const user = this.userProvider.getCurrentUser();

        if (!user) {
            this.router.navigate(['/authenticate']);
        }
    }
}
