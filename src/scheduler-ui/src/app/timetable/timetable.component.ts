import { Observable, takeUntil } from 'rxjs';

import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

import { TimeTableGatewayView } from '../core/models/timetable-gateway-view';
import { selectStudent } from '../core/state/student/student.selectors';
import { BaseComponent } from '../shared/components/base/base.component';
import { toTimeOnlyNormalStyle } from '../utils/date';
import { TimetableService } from './services/timetable.service';

@Component({
    selector: 'uni-timetable',
    templateUrl: './timetable.component.html',
    styleUrl: './timetable.component.scss',
})
export class TimetableComponent extends BaseComponent implements OnInit {
    timeTable$: Observable<TimeTableGatewayView>;

    constructor(
        private timetableService: TimetableService,
        private store: Store
    ) {
        super();
    }

    ngOnInit(): void {
        this.store
            .select(selectStudent)
            .pipe(takeUntil(this.destroy$))
            .subscribe(student => {
                this.timeTable$ = this.timetableService.getFacultyTimetable(
                    student.facultyId
                );
            });
    }

    convertToNormalTime(timeString: string): string {
        return toTimeOnlyNormalStyle(timeString);
    }
}
