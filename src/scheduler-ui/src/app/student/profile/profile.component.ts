import { Observable } from 'rxjs';

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';

import { AuthService } from '../../auth/services/auth.service';
import { StudentGatewayView } from '../../core/models/student-gateway-view';
import { selectStudent } from '../../core/state/student/student.selectors';

@Component({
  selector: 'uni-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {
    student$: Observable<StudentGatewayView>;

    constructor(private store: Store, private auth: AuthService, private router: Router) {}

    ngOnInit(): void {
        this.student$ = this.store.select(selectStudent);
    }

    logout() {
        this.auth.logout();
        window.location.href = "/authenticate"
    }
}
