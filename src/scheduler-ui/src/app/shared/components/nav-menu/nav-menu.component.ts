import { Observable } from 'rxjs';

import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

import { StudentGatewayView } from '../../../core/models/student-gateway-view';
import { mockUser } from '../../../core/models/user-view';
import { MediaService } from '../../../core/services/media.service';
import { loadStudent } from '../../../core/state/student/student.actions';
import { selectStudent } from '../../../core/state/student/student.selectors';

@Component({
    selector: 'uni-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrl: './nav-menu.component.scss',
})
export class NavMenuComponent implements OnInit {
    student$: Observable<StudentGatewayView>;
    user = mockUser

    constructor(private media: MediaService, private store: Store) {
        this.student$ = this.store.select(selectStudent);
    }

    ngOnInit(): void {
        this.store.dispatch(loadStudent());
    }

    showUserInfo = false;

    toggleUserInfo() {
        this.showUserInfo = !this.showUserInfo;
    }
}
