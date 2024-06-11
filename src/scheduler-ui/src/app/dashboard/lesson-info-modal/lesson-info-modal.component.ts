import { Component, Input } from '@angular/core';

import { LessonGatewayView } from '../../core/models/schedule';

@Component({
    selector: 'uni-lesson-info-modal',
    templateUrl: './lesson-info-modal.component.html',
})
export class LessonInfoModalComponent {
    @Input() lesson: LessonGatewayView;
}
