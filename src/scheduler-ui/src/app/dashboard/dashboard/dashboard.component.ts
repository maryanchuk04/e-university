import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

import { loadStudent } from '../../core/state/student/student.actions';

@Component({
    selector: 'uni-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrl: './dashboard.component.scss',
})
export class DashboardComponent implements OnInit {
    constructor(private store: Store) {}

    ngOnInit(): void {
        this.store.dispatch(loadStudent());
    }
}
