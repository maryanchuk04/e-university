import { Component, Input } from '@angular/core';

import { LessonType } from '../../../core/models/my-day-gateway-view';
import { LessonGatewayView } from '../../../core/models/schedule';
import { toTimeOnlyNormalStyle } from '../../../utils/date';

@Component({
  selector: 'uni-lesson-item',
  templateUrl: './lesson-item.component.html',
  styleUrl: './lesson-item.component.scss'
})
export class LessonItemComponent {
    @Input() lesson: LessonGatewayView;

    LessonType = LessonType;

    convertToNormalTime(time) {
        return toTimeOnlyNormalStyle(time);
    }
}
