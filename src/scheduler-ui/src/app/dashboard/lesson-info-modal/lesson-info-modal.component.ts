import { Component, Input } from '@angular/core';

import { LessonView } from '../../core/models/lesson-view';

@Component({
    selector: 'uni-lesson-info-modal',
    templateUrl: './lesson-info-modal.component.html',
})
export class LessonInfoModalComponent {
    @Input() lesson: LessonView;
}
