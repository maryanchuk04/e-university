import { Component, Input } from '@angular/core';

import { LessonType } from '../../../core/models/my-day-gateway-view';
import { LessonGatewayView } from '../../../core/models/schedule';
import { LessonStatus } from '../../../core/models/timetable-gateway-view';
import { toTimeOnlyNormalStyle } from '../../../utils/date';

@Component({
  selector: 'uni-lesson-item',
  templateUrl: './lesson-item.component.html',
  styleUrl: './lesson-item.component.scss'
})
export class LessonItemComponent {
    @Input() status: LessonStatus | null;
    @Input() lesson: LessonGatewayView;

    LessonStatus = LessonStatus;
    LessonType = LessonType;

    convertToNormalTime(time) {
        return toTimeOnlyNormalStyle(time);
    }
}
