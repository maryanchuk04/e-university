import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

import { MediaService, ScreenSize } from '../../core/services/media.service';
import { loadStudent } from '../../core/state/student/student.actions';

@Component({
    selector: 'uni-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrl: './dashboard.component.scss',
})
export class DashboardComponent implements OnInit {
    screenSize: ScreenSize;

    ScreenSize = ScreenSize;

    constructor(private store: Store, private media: MediaService) {}

    ngOnInit(): void {
        this.store.dispatch(loadStudent());

        this.media.getScreenSize()
            .subscribe((res) => {
                this.screenSize = res;
            });
    }

    isMobile() {
        return this.screenSize && this.screenSize === ScreenSize.XS || this.screenSize === ScreenSize.SM;
    }
}
