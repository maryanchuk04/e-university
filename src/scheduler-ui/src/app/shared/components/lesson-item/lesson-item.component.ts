import { Component, Input, OnInit } from '@angular/core';

import { LessonType } from '../../../core/models/my-day-gateway-view';
import { LessonGatewayView } from '../../../core/models/schedule';
import { LessonStatus } from '../../../core/models/timetable-gateway-view';
import { toTimeOnlyNormalStyle } from '../../../utils/date';

@Component({
    selector: 'uni-lesson-item',
    templateUrl: './lesson-item.component.html',
    styleUrl: './lesson-item.component.scss',
})
export class LessonItemComponent implements OnInit {

    @Input() status: LessonStatus | null;
    @Input() lesson: LessonGatewayView;

    isPassed: boolean = false;

    LessonStatus = LessonStatus;
    LessonType = LessonType;

    ngOnInit(): void {
        console.log(this.lesson)
        this.isPassed = this.checkIfPassed();
    }

    convertToNormalTime(time) {
        return toTimeOnlyNormalStyle(time);
    }

    checkIfPassed() {
        return this.status && this.status === LessonStatus.Passed;
    }
}
