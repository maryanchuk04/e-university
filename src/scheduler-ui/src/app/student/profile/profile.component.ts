import { Observable } from 'rxjs';

import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

import { StudentGatewayView } from '../../core/models/student-gateway-view';
import { selectStudent } from '../../core/state/student/student.selectors';

@Component({
  selector: 'uni-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {
    student$: Observable<StudentGatewayView>;

    constructor(private store: Store) {}

    ngOnInit(): void {
        this.student$ = this.store.select(selectStudent);
    }
}
