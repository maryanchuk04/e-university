import { Component, OnInit } from '@angular/core';

import { AuthService } from '../auth/services/auth.service';

@Component({
  selector: 'uni-logout',
  templateUrl: './logout.component.html',
  styleUrl: './logout.component.scss'
})
export class LogoutComponent implements OnInit {
    constructor(private auth: AuthService) {}

    ngOnInit(): void {
        this.auth.logout();
        setTimeout(() => {
            window.location.href = '/authenticate';
        }, 1000)
    }
}
